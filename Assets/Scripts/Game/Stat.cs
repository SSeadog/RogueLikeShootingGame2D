using System.Collections.Generic;
using UnityEngine;
using static Define;

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
        Data.Stat stat = Managers.Data.statDict[type.ToString()];

        if (type.ToString() == "monster")
        {
            _hp = stat.maxHp;
            _speed = stat.speed;
            _power = stat.power;
        }
        else
        {
            Define.PlayerType playerType = (Define.PlayerType)Managers.Game.PlayerId;
            Data.PlayerStat playerStat = Managers.Data.playerStatDict[playerType.ToString()];

            _hp = playerStat.maxHp;
            _speed = playerStat.speed;
            _power = playerStat.power;

            curWeaponType = (Define.WeaponType)playerStat.weaponId;
        }
    }
}
