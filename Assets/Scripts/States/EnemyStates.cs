using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnState : EnemyState
{
    private float _spawnTime = 1f;
    private float _spawnTimer = 0f;

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
    public override void OnStart(EnemyControllerBase enemyController)
    {
        base.OnStart(enemyController);
        _enemyController.SetAnim("Idle");
    }
    public override void Action()
    {
        _enemyController.SetState(EStateType.MoveState);
    }
}

public class MoveState : EnemyState
{
    public override void OnStart(EnemyControllerBase enemyController)
    {
        base.OnStart(enemyController);
        _enemyController.SetAnim("Walk");
    }
    public override void Action()
    {
        // 방향 구하고 -> 방향으로 움직이면 flipX true
        // 아니면 flipX false

        Vector3 vec = (_enemyController.Target.transform.position - _enemyController.transform.position).normalized;
        float rotDeg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        rotDeg = rotDeg < 0 ? rotDeg + 360 : rotDeg;

        if (rotDeg > 90 && rotDeg < 270)
            _enemyController.RotateSprite(false);
        else
            _enemyController.RotateSprite(true);

        float distance = (_enemyController.Target.transform.position - _enemyController.transform.position).magnitude;
        if (distance < _enemyController.Stat.AttackRange)
        {
            _enemyController.SetState(EStateType.AttackState);
        }

        Vector2 moveVec = _enemyController.Target.transform.position - _enemyController.transform.position;
        _enemyController.transform.Translate(moveVec.normalized * _enemyController.Stat.Speed * Time.deltaTime);
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
        _enemyController.SetAnim("Attack");
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public override void Action()
    {
        if (_canAttack == false)
            return;

        float distance = (_enemyController.Target.transform.position - _enemyController.transform.position).magnitude;
        if (distance > _enemyController.Stat.AttackRange)
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

        _enemyController.ChangeColor(Color.red);
    }

    public override void Action()
    {
        _getAttackedTimer += Time.deltaTime;
        if (_getAttackedTimer > _enemyController.GetAttackedTime)
            _enemyController.SetState(_beforeStateType);
    }

    public override void OnEnd()
    {
        _enemyController.ChangeColorToBaseColor();
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
    float _dieTime = 0.5f;

    public override void OnStart(EnemyControllerBase enemyController)
    {
        base.OnStart(enemyController);

        _enemyController.GetComponent<Collider2D>().enabled = false;
        _enemyController.SetAnim("Dead");
        _enemyController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Managers.Game.RemoveSpawnedEnemy(_enemyController);
    }

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