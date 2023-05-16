using UnityEngine;

public class PlayerStat : Stat
{
    private Define.WeaponType curWeaponType;
    public Define.WeaponType CurWeaponType { get { return curWeaponType; } }

    public override void Init()
    {
        base.Init();

        Define.ObjectType playerType = (Define.ObjectType)Managers.Game.PlayerId;
        Data.Stat statData = Managers.Data.PlayerStatDict[playerType.ToString()];

        _maxHp = statData.maxHp;
        _hp = _maxHp;
        _speed = statData.speed;
        curWeaponType = (Define.WeaponType)statData.weaponId;

        Managers.Game.LoadWeapon(curWeaponType, transform);
    }
}
