using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyControllerBase : MonoBehaviour
{
    protected Stat _stat;
    protected float _attackRange = 5f; // stat으로 밀어 넣어야하는 데이터
    protected float _attackSpeed = 1f; // stat으로 밀어 넣어야하는 데이터

    protected float _getAttackedTime = 0.2f; // 고정값

    protected GameObject _target;

    protected float _attackTimer = 0f;
    protected bool _canAttack = true;

    protected float _getAttackedTimer = 0f;
    protected bool _canAction = true;

    protected GameObject _bulletRoot;

    SpriteRenderer _spriteRenderer;
    Color _baseColor;

    protected enum State
    {
        Idle,
        Walk,
        Attack,
        Die
    }

    protected State _currentState;

    public virtual void Init()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _stat = GetComponent<Stat>();

        _bulletRoot = GameObject.Find("BulletControll");
        if (_bulletRoot == null)
        {
            _bulletRoot = new GameObject("BulletControll");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;

        _currentState = State.Idle;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (_canAttack == false)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackSpeed)
            {
                _canAttack = true;
                _attackTimer= 0f;
            }
        }

        if (_canAction == false)
        {
            _getAttackedTimer += Time.deltaTime;
            if (_getAttackedTimer > _getAttackedTime)
            {
                _spriteRenderer.color = _baseColor;

                _canAction = true;
                _getAttackedTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (_canAction == false)
            return;

        switch (_currentState)
        {
            case State.Idle:
                {
                    _currentState = State.Walk;
                }
                break;
            case State.Walk:
                {
                    float distance = (_target.transform.position - transform.position).magnitude;
                    if (distance < _attackRange)
                    {
                        _currentState = State.Attack;
                        break;
                    }

                    Move();
                }
                break;
            case State.Attack:
                {
                    float distance = (_target.transform.position - transform.position).magnitude;
                    if (distance > _attackRange)
                    {
                        _currentState = State.Walk;
                        break;
                    }

                    if (_canAttack)
                        Attack();
                }
                break;
            case State.Die:
                Debug.Log("Monster Die!!!!");
                break;
        }
    }

    void Move()
    {
        Vector2 moveVec = _target.transform.position - transform.position;
        transform.Translate(moveVec.normalized * _stat.Speed * Time.deltaTime);
    }

    protected virtual void Attack()
    {
        _canAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _stat.GetDamaged(10f);
            if (_stat.Hp <= 0)
            {
                Die();
            }
            else
            {
                _spriteRenderer.color = Color.red;
                _canAction = false;
                _getAttackedTimer = 0f;
            }
        }
    }

    protected virtual void Die()
    {
        _currentState = State.Die;
    }
}
