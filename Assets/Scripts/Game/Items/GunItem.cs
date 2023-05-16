using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : ItemBase
{
    private Define.WeaponType _weaponType = Define.WeaponType.SniperRifle;
    public override void Effect(Stat stat)
    {
        Managers.Game.LoadWeapon(_weaponType, stat.transform);
    }
}
