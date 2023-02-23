using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    GunBase curWeapon;

    bool isReloading = false;
    float reloadingTimer = 0f;

    float _getAttackedTime = 0.5f;
    bool _canAttacked = true;
    float _getAttackedTimer = 0f;

    float _tumbleTime = 0.3f;
    bool _isTumbling = false;
    float _tumbleTimer = 0f;
    Vector3 _tumbleVec = Vector3.zero;

    SpriteRenderer _spriteRenderer;
    Color _baseColor;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        _stat.onGetDamagedAction += OnAttacekd;
        _stat.onDeadAction += OnDead;

        GameObject ori = Resources.Load<GameObject>("Prefabs/Weapons/" + _stat.CurWeaponType.ToString());
        GameObject instance = Instantiate(ori, transform);
        curWeapon = instance.GetComponent<GunBase>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }

    void Update()
    {
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
        
        if (_isTumbling == true)
        {
            _tumbleTimer += Time.deltaTime;
            if (_tumbleTimer > _tumbleTime)
            {
                _isTumbling = false;
                _tumbleTimer = 0f;
            }
        }

        Tumble();
        Fire();
        Reload();
    }

    void FixedUpdate()
    {
        Move();
        RotateGun();
    }

    void Tumble()
    {
        if (Input.GetMouseButtonDown(1))
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            _tumbleVec = new Vector2(xAxis, yAxis).normalized;
        }
    }

    void Fire()
    {
        if (isReloading)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (curWeapon.GetCurLoadedAmmo() == 0)
            {
                Debug.Log("Reload!!!");
                isReloading = true;
            }
            else
            {
                curWeapon.Fire();
            }
        }
    }

    void Reload()
    {
        if (isReloading)
        {
            reloadingTimer += Time.deltaTime;

            if (reloadingTimer > curWeapon.GetReloadingTime())
            {
                Debug.Log("Reload Done!!!");
                curWeapon.Reload();
                isReloading = false;
                reloadingTimer = 0f;
            }
        }
    }

    void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized;

        transform.Translate(moveVec * Time.deltaTime * _stat.Speed);
    }

    void RotateGun()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - curWeapon.transform.position.y, mouse.x - curWeapon.transform.position.x) * Mathf.Rad2Deg;
        curWeapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canAttacked == false)
            return;

        if (collision.CompareTag("EnemyBullet"))
        {
            _stat.GetDamaged(10f);
        }
    }

    void OnAttacekd()
    {
        _spriteRenderer.color = Color.red;
        _canAttacked = false;
        _getAttackedTimer = 0f;
    }

    void OnDead()
    {
        Debug.Log("Player Dead!!!");
        SceneManager.LoadScene("TestStartScene");
    }
}
