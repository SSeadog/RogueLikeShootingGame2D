using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class EnemyControllerBase : MonoBehaviour
{
    public Stat stat;
    public float attackRange = 5f; // stat���� �о� �־���ϴ� ������
    public float attackSpeed = 1f; // stat���� �о� �־���ϴ� ������
    public float getAttackedTime = 0.2f; // ������
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

        SetState(new MoveState());
    }

    void Start()
    {
        Init();
    }

    public void SetState(EnemyState state)
    {
        _state?.OnEnd();
        _state = state;
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
        AttackedState state = new AttackedState();
        state.SetBeforeState(_state);
        SetState(state);
        
    }

    protected virtual void OnDead()
    {
        SetState(new DieState());
    }
}

public class EnemyState
{
    protected EnemyControllerBase _enemyController;

    public virtual void OnStart(EnemyControllerBase enemyController) { _enemyController = enemyController; }

    public virtual void Action() { }

    public virtual void OnEnd() { }
}

public class SpawnState : EnemyState
{
    float _spawnTime = 1f;
    float _spawnTimer = 0f;

    public override void Action()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTime)
            _enemyController.SetState(new MoveState());
    }
}

public class MoveState : EnemyState
{
    public override void Action()
    {
        float distance = (_enemyController.target.transform.position - _enemyController.transform.position).magnitude;
        if (distance < _enemyController.attackRange)
        {
            _enemyController.SetState(new AttackState());
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
    float _attackTimer;

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
        float distance = (_enemyController.target.transform.position - _enemyController.transform.position).magnitude;
        if (distance > _enemyController.attackRange)
        {
            _enemyController.SetState(new MoveState());
        }

        if (_canAttack == true)
        {
            // attack�� ��Ÿ���� �ް� ���� ��Ÿ�Ӱ� �ٸ��ٸ� ��Ÿ�� ������ �ٲٰ� �ش� �ð���ŭ ��ٸ��� �����
            float coolTime = _enemyController.Attack();
            _attackCoolTime = coolTime;
            _canAttack = false;
        }

        if (_canAttack == false)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackCoolTime)
            {
                _canAttack = true;
                _attackTimer = 0f;
            }
        }
    }
}

// �ٸ� ���·� ��ȯ�� �� ���� ��Ÿ���� �ʱ�ȭ�� ��
// ���� ���¿��� �� ������ �ؼ� �׷� ���ε�
// �ٸ� ������� �ؾ��� ��? �ڷ�ƾ���� ������Ʈ�ѷ����� �ñ�ٴ��� �׷�������
public class AttackedState : EnemyState
{
    EnemyState _beforeState;
    float _getAttackedTimer = 0f;

    public void SetBeforeState(EnemyState state)
    {
        if (state is not AttackedState)
            _beforeState = state;
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
            _enemyController.SetState(_beforeState);
    }

    public override void OnEnd()
    {
        _enemyController._spriteRenderer.color = _enemyController._baseColor;
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
            _enemyController.SetState(null);
            GameObject coin = Managers.Resource.Instantiate("Prefabs/Items/Coin");
            coin.transform.position = _enemyController.transform.position;
            GameObject.Destroy(_enemyController.gameObject);
        }
    }
}