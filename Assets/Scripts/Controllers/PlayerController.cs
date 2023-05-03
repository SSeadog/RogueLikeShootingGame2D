using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    None,
    Normal,
    Move,
    Action,
    Fall,
    Dead
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _leftHand;
    [SerializeField] Transform _rightHand;

    private PlayerStat _stat;
    private WeaponBase _curWeapon;

    private bool _isReloading = false;
    private float _reloadingTimer = 0f;

    private float _getAttackedTime = 0.5f;
    private bool _canAttacked = true;
    private float _getAttackedTimer = 0f;

    private float _tumbleTime = 0.5f;
    private bool _isTumbling = false;
    private float _tumbleTimer = 0f;

    private Color _baseColor;
    private Vector3 _dropEnterPoint;
    private GameObject _explodeEffect;
    private SpriteRenderer[] _spriteRenderers;
    private PlayerState _state = PlayerState.Normal;
    private Transform _characterTransform;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _stat.Init();
        _stat.onGetDamagedAction += OnAttacekd;
        _stat.onDeadAction += OnDead;

        _explodeEffect = Managers.Resource.Load("Prefabs/Weapons/Grenade");

        _curWeapon = Managers.Game.PlayerWeaponList[0];

        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _baseColor = Color.white;

        Managers.Ui.GetUI<HpBarUI>().SetHpBar((int)_stat.Hp);

        _characterTransform = transform.Find("Character");
        _animator = _characterTransform.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_state == PlayerState.Normal || _state == PlayerState.Move)
        {
            Tumble();
            Fire();
            Reload();
            ExplodeGrenade();
        }
        else if (_state == PlayerState.Action)
        {
            // 액션 중일때는 일반 동작들 못하게 막기
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
                ChangeColor(Color.white);

                _canAttacked = true;
                _getAttackedTimer = 0f;
            }
        }

        if (_isReloading)
        {
            _reloadingTimer += Time.deltaTime;

            if (_reloadingTimer > _curWeapon.GetReloadingTime())
            {
                _curWeapon.Reload();
                _isReloading = false;
                _reloadingTimer = 0f;
            }
        }

        Anims();

        _leftHand.rotation = Quaternion.LookRotation(_curWeapon.LeftHandPoint.position - _leftHand.position) * Quaternion.Euler(0f, -90f, 0f);
        _rightHand.rotation = Quaternion.LookRotation(_curWeapon.RightHandPoint.position - _rightHand.position) * Quaternion.Euler(0f, -90f, 0f);

        // 폭탄 몹 때문에 밀려날 때 충돌 없애는 로직
        // 특정 레이어(몬스터)만 피하도록 수정 필요할 듯
        if (_collider.enabled)
        {
            if (_rigidbody.velocity != Vector2.zero)
                _collider.enabled = false;
        }
        else
        {
            if (_rigidbody.velocity == Vector2.zero)
                _collider.enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (_isTumbling)
            return;

        Move();
        Rotate();
    }

    void Anims()
    {
        switch (_state)
        {
            case PlayerState.Normal:
                _animator.Play("GunIdle");
                break;
            case PlayerState.Move:
                _animator.Play("GunWalk");
                break;
            case PlayerState.Action:
                _animator.Play("Roll");
                break;
            case PlayerState.Fall:
                _animator.Play("Fall");
                break;
            case PlayerState.Dead:
                _animator.Play("Die");
                break;
        }
    }

    void Tumble()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            StartCoroutine(CoTumble());
        }
    }

    IEnumerator CoTumble()
    {
        _state = PlayerState.Action;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullet"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        _curWeapon.SetVisible(false);
        _isTumbling = true;

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector3 tumbleVec = new Vector3(xAxis, yAxis, 0f).normalized;

        float angle = Mathf.Atan2(yAxis, xAxis) * Mathf.Rad2Deg;
        // z가 90보다 크고 270보다 작을 때만 플립
        angle = angle < 0 ? angle + 360 : angle;

        if (angle > 90 && angle < 270)
            _characterTransform.rotation = Quaternion.Euler(0, 180, 0);
        else
            _characterTransform.rotation = Quaternion.Euler(0, 0, 0);

        while (_tumbleTimer < _tumbleTime)
        {
            _tumbleTimer += Time.deltaTime;
            Vector3 tumbleTick = tumbleVec * Time.deltaTime * 2.5f * _stat.Speed;
            transform.position = transform.position + tumbleTick;

            yield return null;
        }

        _state = PlayerState.Normal;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullet"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        _curWeapon.SetVisible(true);
        _isTumbling = false;
        _tumbleTimer = 0f;
    }

    void Fire()
    {
        if (_isReloading)
            return;

        if (Input.GetMouseButton(0))
        {
            if (_curWeapon.CurLoadAmmo == 0)
            {
                if (Input.GetMouseButtonDown(0))
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
            if (Managers.Game.Grenade < 1)
                return;

            GameObject instance = Instantiate(_explodeEffect, transform.position, Quaternion.identity);
            instance.GetComponent<Grenade>().Explode();
            Managers.Game.Grenade--;
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

        if (moveVec != Vector2.zero)
            _state = PlayerState.Move;
        else
            _state = PlayerState.Normal;
    }

    void Rotate()
    {
        if (_curWeapon == null)
            return;

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - _curWeapon.transform.position.y, mouse.x - _curWeapon.transform.position.x) * Mathf.Rad2Deg;
        // z가 90보다 크고 270보다 작을 때만 플립
        angle = angle < 0 ? angle + 360 : angle;

        if (angle > 90 && angle < 270)
        {
            _curWeapon.Flip(true);
            _characterTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _curWeapon.Flip(false);
            _characterTransform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _curWeapon.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canAttacked == false)
            return;

        if (collision.CompareTag("EnemyBullet"))
        {
            //_stat.GetDamaged(1);
        }

        if (collision.gameObject.GetComponent<ItemBase>() != null)
        {
            collision.gameObject.GetComponent<ItemBase>().GetItem(transform);
        }

        if (collision.CompareTag("UnderGround"))
        {
            _dropEnterPoint = transform.position;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("UnderGround"))
        {
            if (_state == PlayerState.Fall)
                return;

            // 구르는 중이면 빠져나가기
            if (_isTumbling)
                return;

            Drop();
        }
    }

    void Drop()
    {
        StartCoroutine(CoDrop());
    }

    IEnumerator CoDrop()
    {
        _state = PlayerState.Fall;
        while (transform.localScale.magnitude > 0.05f)
        {
            transform.localScale = transform.localScale * 0.95f;
            yield return null;
        }

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

    void ChangeColor(Color color)
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = color;
        }
    }

    void OnAttacekd()
    {
        ChangeColor(Color.red);
        _canAttacked = false;
        _getAttackedTimer = 0f;
        Managers.Ui.GetUI<HpBarUI>().SetHpBar((int)_stat.Hp);
    }

    void OnDead()
    {
        Managers.Scene.LoadScene("StartScene");
    }
}
