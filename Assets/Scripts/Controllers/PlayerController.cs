using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    WeaponBase _curWeapon;

    GameObject _explodeEffect;

    bool _isReloading = false;
    float _reloadingTimer = 0f;

    float _getAttackedTime = 0.5f;
    bool _canAttacked = true;
    float _getAttackedTimer = 0f;

    float _tumbleTime = 0.5f;
    bool _isTumbling = false;
    float _tumbleTimer = 0f;

    SpriteRenderer _spriteRenderer;
    Color _baseColor;

    Vector3 _dropEnterPoint;

    enum PlayerState
    {
        None,
        Normal,
        Action,
        Fall,
        Dead
    }
    PlayerState _state = PlayerState.Normal;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _stat.Init();
        _stat.onGetDamagedAction += OnAttacekd;
        _stat.onDeadAction += OnDead;

        _explodeEffect = Managers.Resource.Load("Prefabs/Weapons/Grenade");

        _curWeapon = Managers.Game.playerWeaponList[0];

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;

        Managers.Ui.GetUI<HpBarUI>().SetHpBar((int)_stat.Hp);
    }

    void Update()
    {
        if (_state == PlayerState.Normal)
        {
            Tumble();
            Fire();
            Reload();
            ExplodeGrenade();
        }
        else if (_state == PlayerState.Action)
        {
            // 액션 중일때는 일반 동작들 못하게 막기
            if (_isTumbling == true)
            {
                _tumbleTimer += Time.deltaTime;
                if (_tumbleTimer > _tumbleTime)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullet"), false);
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

                    _isTumbling = false;
                    _tumbleTimer = 0f;
                    _state = PlayerState.Normal;
                }
            }
        }
        else if (_state == PlayerState.Fall)
        {
            // 떨어지는 중일 때도 일반 동작들 못하게 막기
        }
        else if (_state == PlayerState.Dead)
        {
            // 죽은 상태에서도 일반 동작들 못하게 막기
        }

        if (_canAttacked == false)
        {
            _getAttackedTimer += Time.deltaTime;
            if (_getAttackedTimer > _getAttackedTime)
            {
                _spriteRenderer.color = _baseColor;

                _canAttacked = true;
                _getAttackedTimer = 0f;
            }
        }

        if (_isReloading)
        {
            _reloadingTimer += Time.deltaTime;

            if (_reloadingTimer > _curWeapon.GetReloadingTime())
            {
                Debug.Log("Reload Done!!!");
                _curWeapon.Reload();
                _isReloading = false;
                _reloadingTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        // Update쪽에서는 if문으로 갈랐는데 여기서도 똑같이 갈라주면 되나..?
        if (_isTumbling)
            return;

        Move();
        RotateGun();
    }

    void Tumble()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullet"), true);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

            _state = PlayerState.Action;
            _isTumbling = true;
            StartCoroutine(CoTumble());
        }
    }

    IEnumerator CoTumble()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector3 tumbleVec = new Vector3(xAxis, yAxis, 0f).normalized;
        float magnitude = 0f;

        while (magnitude < 5.5f)
        {
            Vector3 tumbleTick = tumbleVec * Time.deltaTime * 2.5f * _stat.Speed;
            transform.position = transform.position + tumbleTick;
            magnitude += tumbleTick.magnitude;

            yield return null;
        }
    }

    void Fire()
    {
        if (_isReloading)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (_curWeapon.CurLoadAmmo == 0)
            {
                Reload(true);
            }
            else
            {
                _curWeapon.Fire();
            }
        }
    }

    void Reload(bool must = false)
    {
        if (must || (Input.GetKeyDown(KeyCode.R) && _curWeapon.CurLoadAmmo < _curWeapon.FullLoadAmmo))
        {
            _isReloading = true;
            transform.GetComponentInChildren<ReloadGaugeUI>().FillGauge(_curWeapon.GetReloadingTime());
        }
    }

    void ExplodeGrenade()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instance = Instantiate(_explodeEffect, transform.position, Quaternion.identity);
            instance.GetComponent<Grenade>().Explode();
        }
    }

    void Move()
    {
        if (_isTumbling)
            return;

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized;

        transform.Translate(moveVec * Time.deltaTime * _stat.Speed);
    }

    void RotateGun()
    {
        if (_curWeapon == null)
            return;

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - _curWeapon.transform.position.y, mouse.x - _curWeapon.transform.position.x) * Mathf.Rad2Deg;
        // z가 90보다 크고 270보다 작을 때만 플립
        angle = angle < 0 ? angle + 360 : angle;

        if (angle > 90 && angle < 270)
            _curWeapon.Flip(true);
        else
            _curWeapon.Flip(false);

        _curWeapon.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canAttacked == false)
            return;

        if (collision.CompareTag("EnemyBullet"))
        {
            // 플레이어가 받는 데미지는 고정값
            //_stat.GetDamaged(1);
        }

        if (collision.gameObject.GetComponent<ItemBase>() != null)
        {
            collision.gameObject.GetComponent<ItemBase>().GetItem(transform);
        }

        if (collision.gameObject.tag == "UnderGround")
        {
            // 위치 저장
            _dropEnterPoint = transform.position;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UnderGround")
        {
            if (_state == PlayerState.Fall)
                return;

            // 구르는 중이면 빠져나가기
            if (_isTumbling)
                return;

            // 판정이 콜라이더가 닫아있기만 해도 떨어지기 때문에 적절히 타일맵 콜라이더 수정해주기
            _state = PlayerState.Fall;
            Drop();
        }
    }

    void Drop()
    {
        StartCoroutine(CoDrop());
    }

    IEnumerator CoDrop()
    {
        while (transform.localScale.magnitude > 0.05f)
        {
            transform.localScale = transform.localScale * 0.95f;
            yield return null;
        }

        //Destroy(gameObject);
        // hp 감소
        yield return new WaitForSeconds(0.5f);
        Respawn();
    }

    void Respawn()
    {
        Vector3 respawnPoint = _dropEnterPoint + (_dropEnterPoint - transform.position).normalized;
        transform.localScale = Vector3.one;
        transform.position = respawnPoint;
        _state = PlayerState.Normal;
    }

    void OnAttacekd()
    {
        _spriteRenderer.color = Color.red;
        _canAttacked = false;
        _getAttackedTimer = 0f;
        Managers.Ui.GetUI<HpBarUI>().SetHpBar((int)_stat.Hp);
    }

    void OnDead()
    {
        Debug.Log("Player Dead!!!");
        Managers.Scene.LoadScene("TestStartScene");
    }
}
