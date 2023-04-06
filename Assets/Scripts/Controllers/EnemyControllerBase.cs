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
    public EnemyStat stat;
    public float getAttackedTime = 0.2f; // 고정값
    public GameObject target;

    public SpriteRenderer _spriteRenderer;
    public Color _baseColor;

    protected EnemyState _state;
    protected GameObject _bulletRoot;

    EStateType _curStateType;
    Dictionary<EStateType, EnemyState> _states = new Dictionary<EStateType, EnemyState>();

    public virtual void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        stat = GetComponent<EnemyStat>();
        stat.Init();

        stat.onGetDamagedAction += OnDamaged;
        stat.onDeadAction += OnDead;

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
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        _state?.Action();
    }

    public abstract float Attack();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            stat.GetDamaged(10f);
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

public class SpawnState : EnemyState
{
    float _spawnTime = 1f;
    float _spawnTimer = 0f;

    public override void Action()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTime)
            _enemyController.SetState(EStateType.MoveState);
    }

    public override void Clear()
    {
        _spawnTimer = 0f;
    }
}

public class IdleState : EnemyState
{
    public override void Action()
    {
        _enemyController.SetState(EStateType.MoveState);
    }
}

public class MoveState : EnemyState
{
    public override void Action()
    {
        float distance = (_enemyController.target.transform.position - _enemyController.transform.position).magnitude;
        if (distance < _enemyController.stat.AttackRange)
        {
            _enemyController.SetState(EStateType.AttackState);
        }

        Vector2 moveVec = _enemyController.target.transform.position - _enemyController.transform.position;
        _enemyController.transform.Translate(moveVec.normalized * _enemyController.stat.Speed * Time.deltaTime);
    }
}

public class AttackState : EnemyState
{
    GameObject _target;
    protected bool _canAttack = true;
    float _attackCoolTime;

    public override void OnStart(EnemyControllerBase enemyController)
    {
        base.OnStart(enemyController);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public override void Action()
    {
        if (_canAttack == false)
            return;

        float distance = (_enemyController.target.transform.position - _enemyController.transform.position).magnitude;
        if (distance > _enemyController.stat.AttackRange)
        {
            _enemyController.SetState(EStateType.MoveState);
            return;
        }

        // attack별 쿨타임을 받고 지금 쿨타임과 다르다면 쿨타임 세팅을 바꾸고 해당 시간만큼 기다리게 만들기
        float coolTime = _enemyController.Attack();
        _attackCoolTime = coolTime;
        _canAttack = false;
        _enemyController.StartCoroutine(CoCoolDown(coolTime));
    }

    public IEnumerator CoCoolDown(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        _canAttack = true;
    }

    public override void Clear()
    {
        _target = null;
        _canAttack = true;
        _attackCoolTime = 0f;
    }
}

public class AttackedState : EnemyState
{
    EStateType _beforeStateType = EStateType.IdleState;
    float _getAttackedTimer = 0f;

    public void SetBeforeState(EStateType type)
    {
        if (type is not EStateType.AttackedState)
            _beforeStateType = type;
    }

    public override void OnStart(EnemyControllerBase enemyController)
    {
        base.OnStart(enemyController);

        _enemyController._spriteRenderer.color = Color.red;
    }

    public override void Action()
    {
        _getAttackedTimer += Time.deltaTime;
        if (_getAttackedTimer > _enemyController.getAttackedTime)
            _enemyController.SetState(_beforeStateType);
    }

    public override void OnEnd()
    {
        _enemyController._spriteRenderer.color = _enemyController._baseColor;
    }

    public override void Clear()
    {
        _beforeStateType = EStateType.IdleState;
        _getAttackedTimer = 0f;
    }
}

public class DieState : EnemyState
{
    float _timer = 0f;
    float _dieTime = 1f;

    public override void Action()
    {
        _timer += Time.deltaTime;
        if (_timer >= _dieTime)
        {
            GameObject coin = Managers.Resource.Instantiate("Prefabs/Items/Coin");
            coin.transform.position = _enemyController.transform.position;
            GameObject.Destroy(_enemyController.gameObject);
        }
    }

    public override void Clear()
    {
        _timer = 0f;
        _dieTime = 1f;
    }
}