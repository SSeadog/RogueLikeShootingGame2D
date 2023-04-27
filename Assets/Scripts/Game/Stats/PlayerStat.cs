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

        _hp = statData.maxHp;
        _speed = statData.speed;
        curWeaponType = (Define.WeaponType)statData.weaponId;

        GameObject weapon = Managers.Resource.Instantiate("Prefabs/Weapons/" + CurWeaponType.ToString(), transform);
        Managers.Game.PlayerWeaponList.Add(weapon.GetComponent<WeaponBase>());
    }
}
