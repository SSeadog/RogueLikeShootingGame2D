using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class EnemyControllerBase : MonoBehaviour
{
    public Stat stat;
    public float attackRange = 5f; // stat으로 밀어 넣어야하는 데이터
    public float attackSpeed = 1f; // stat으로 밀어 넣어야하는 데이터
    public float getAttackedTime = 0.2f; // 고정값
    public GameObject target;

    public SpriteRenderer _spriteRenderer;
    public Color _baseColor;

    protected EnemyState _state;
    protected GameObject _bulletRoot;

    public virtual void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        stat = GetComponent<Stat>();
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

        SetState(new MoveState(this));
    }

    void Start()
    {
        Init();
    }

    public void SetState(EnemyState state)
    {
        if (_state != null)
            _state.OnEnd();

        _state = state;
        _state.OnStart();
    }

    void Update()
    {
        // TestCode
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        _state.Action();
    }

    public abstract void Attack();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            stat.GetDamaged(10f);
        }
    }

    protected virtual void OnDamaged()
    {
        AttackedState state = new AttackedState(this);
        state.SetBeforeState(_state);
        SetState(state);
        
    }

    protected virtual void OnDead()
    {
        SetState(new DieState(this));
        Destroy(gameObject, 1f);
    }
}

public class EnemyState
{
    public EnemyControllerBase enemyController;
    public EnemyState(EnemyControllerBase enemyController)
    {
        this.enemyController = enemyController;
    }

    public virtual void OnStart() { }

    public virtual void Action() { }

    public virtual void OnEnd() { }
}

public class SpawnState : EnemyState
{
    float _spawnTime = 1f;
    float _spawnTimer = 0f;

    public SpawnState(EnemyControllerBase enemyController) : base(enemyController)
    {
    }

    public override void Action()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTime)
            enemyController.SetState(new MoveState(enemyController));
    }
}

public class MoveState : EnemyState
{
    public MoveState(EnemyControllerBase enemyController) : base(enemyController)
    {
    }


    public override void Action()
    {
        float distance = (enemyController.target.transform.position - enemyController.transform.position).magnitude;
        if (distance < enemyController.attackRange)
        {
            enemyController.SetState(new AttackState(enemyController));
        }

        Vector2 moveVec = enemyController.target.transform.position - enemyController.transform.position;
        enemyController.transform.Translate(moveVec.normalized * enemyController.stat.Speed * Time.deltaTime);
    }
}

public class AttackState : EnemyState
{
    GameObject _target;
    protected bool _canAttack = true;
    float _attackTimer;

    public AttackState(EnemyControllerBase enemyController) : base(enemyController)
    {
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public override void Action()
    {
        float distance = (enemyController.target.transform.position - enemyController.transform.position).magnitude;
        if (distance > enemyController.attackRange)
        {
            enemyController.SetState(new MoveState(enemyController));
        }

        if (_canAttack == true)
        {
            enemyController.Attack();
            _canAttack = false;
        }

        if (_canAttack == false)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > enemyController.attackSpeed)
            {
                _canAttack = true;
                _attackTimer = 0f;
                Debug.Log("Attack CoolTime Done");
            }
        }
    }
}

public class AttackedState : EnemyState
{
    EnemyState _beforeState;
    float _getAttackedTimer = 0f;
    public AttackedState(EnemyControllerBase enemyController) : base(enemyController)
    {
    }

    public void SetBeforeState(EnemyState state)
    {
        if (state is not AttackedState)
            _beforeState = state;
    }

    public override void OnStart()
    {
        enemyController._spriteRenderer.color = Color.red;
    }

    public override void Action()
    {
        _getAttackedTimer += Time.deltaTime;
        if (_getAttackedTimer > enemyController.getAttackedTime)
            enemyController.SetState(_beforeState);
    }

    public override void OnEnd()
    {
        enemyController._spriteRenderer.color = enemyController._baseColor;
    }
}

public class DieState : EnemyState
{
    public DieState(EnemyControllerBase enemyController) : base(enemyController)
    {
    }

    public override void Action()
    {

    }
}