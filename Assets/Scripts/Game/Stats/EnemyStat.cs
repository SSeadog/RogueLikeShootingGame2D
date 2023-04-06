using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    float _attackRange;
    float _attackSpeed;

    public float AttackRange { get { return _attackRange; } }
    public float AttackSpeed { get { return _attackSpeed; } }

    public override void Init()
    {
        base.Init();

        Data.MonsterStat statData = Managers.Data.monsterStatDict[type.ToString()];

        _maxHp = statData.maxHp;
        _hp = _maxHp;
        _speed = statData.speed;
        _attackRange = statData.attackRange;
        _attackSpeed = statData.attackSpeed;
    }
}
