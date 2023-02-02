using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] Define.ObjectType type;

    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _power;

    [SerializeField] Define.WeaponType curWeaponType;

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public float Power { get { return _power;} }
    public Define.WeaponType CurWeaponType { get { return curWeaponType; } }

    public virtual void Init()
    {

        if ((int)type > 10000)
        {
            Data.Stat stat = Managers.Data.monsterStatDict[type.ToString()];

            _hp = stat.maxHp;
            _speed = stat.speed;
            _power = stat.power;
        }
        else
        {
            Define.ObjectType playerType = (Define.ObjectType)Managers.Game.PlayerId;
            Data.Stat stat = Managers.Data.playerStatDict[playerType.ToString()];

            _hp = stat.maxHp;
            _speed = stat.speed;
            _power = stat.power;

            curWeaponType = (Define.WeaponType)stat.weaponId;
        }
    }

    public void GetDamaged(float damage)
    {
        _hp -= damage;
    }
}
