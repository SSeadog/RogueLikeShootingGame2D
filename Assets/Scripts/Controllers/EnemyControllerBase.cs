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

    private float _getAttackedTime = 0.2f; // °íÁ¤°ª
    private GameObject _target;

    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;

    private EStateType _curStateType;
    private Dictionary<EStateType, EnemyState> _states = new Dictionary<EStateType, EnemyState>();

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

        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    void Update()
    {
        // TestCode
        if (_target == null)
            _target = GameObject.FindGameObjectWithTag("Player");

        _state?.Action();
    }

    public abstract float Attack();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _stat.GetDamaged(10f);
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