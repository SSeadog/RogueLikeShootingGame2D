using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerStat : Stat
{
    Define.WeaponType curWeaponType;
    public Define.WeaponType CurWeaponType { get { return curWeaponType; } }

    public override void Init()
    {
        base.Init();

        Define.ObjectType playerType = (Define.ObjectType)Managers.Game.PlayerId;
        Data.Stat statData = Managers.Data.playerStatDict[playerType.ToString()];

        _hp = statData.maxHp;
        _speed = statData.speed;
        curWeaponType = (Define.WeaponType)statData.weaponId;

        GameObject weapon = Managers.Resource.Instantiate("Prefabs/Weapons/" + CurWeaponType.ToString(), transform);
        Managers.Game.playerWeaponList.Add(weapon.GetComponent<WeaponBase>());
    }
}
