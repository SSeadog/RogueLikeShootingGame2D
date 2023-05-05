using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStateType
{
    SpawnState = -1,
    IdleState,
    MoveState,
    AttackState,
    AttackedState,
    DieState,
    Max
}

public abstract class EnemyControllerBase : MonoBehaviour
{
    protected EnemyStat _stat;
    protected EnemyState _state;
    protected GameObject _bulletRoot;
    protected GameObject _target;

    private Dictionary<EStateType, EnemyState> _states = new Dictionary<EStateType, EnemyState>();
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    private EStateType _curStateType;
    private float _getAttackedTime = 0.2f; // °íÁ¤°ª
    private Animator _animator;

    public EnemyStat Stat { get { return _stat; } }
    public GameObject Target { get { return _target; } }
    public float GetAttackedTime { get { return _getAttackedTime; } }


    public virtual void Init()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _stat = GetComponent<EnemyStat>();
        _stat.Init();

        _stat.onGetDamagedAction += OnDamaged;
        _stat.onDeadAction += OnDead;

        _bulletRoot = GameObject.Find("BulletControll");
        if (_bulletRoot == null)
        {
            _bulletRoot = new GameObject("BulletControll");
        }

        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;

        for (int i = (int)EStateType.SpawnState; i < (int)EStateType.Max; i++)
        {
            Type type = Type.GetType(((EStateType)i).ToString());
            _states[(EStateType)i] = (EnemyState) Activator.CreateInstance(type);
        }

        SetState(EStateType.SpawnState);
    }

    void Start()
    {
        Init();
    }

    public void SetState(EStateType stateType)
    {
        _state?.OnEnd();
        if (_state is not AttackState)
            _state?.Clear();
        _curStateType = stateType;
        _state = _states[stateType];
        _state?.OnStart(this);
    }

    public void SetAnim(string animName)
    {
        _animator.Play(animName);
    }

    public void RotateSprite(bool rotateX)
    {
        _spriteRenderer.flipX = rotateX;
    }

    void Update()
    {
        // TestCode
        if (_target == null)
            _target = GameObject.FindGameObjectWithTag("Player");

        _state?.Action();
    }

    public abstract float Attack();

    public void ChangeColorToBaseColor()
    {
        _spriteRenderer.color = _baseColor;
    }

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            float power = collision.gameObject.GetComponent<Bullet>().Power;
            _stat.GetDamaged(power);
        }
    }

    protected virtual void OnDamaged()
    {
        (_states[EStateType.AttackedState] as AttackedState).SetBeforeState(_curStateType);
        SetState(EStateType.AttackedState);
    }

    protected virtual void OnDead()
    {
        SetState(EStateType.DieState);
        Managers.Game.KillCount++;
    }
}

public class EnemyState
{
    protected EnemyControllerBase _enemyController;

    public virtual void OnStart(EnemyControllerBase enemyController) { _enemyController = enemyController; }

    public virtual void Action() { }

    public virtual void OnEnd() { }

    public virtual void Clear() { }
}