using UnityEngine;

public class PlayerStat : Stat
{
    private Define.WeaponType _curWeaponType;
    private Color _baseColor;
    public Define.WeaponType CurWeaponType { get { return _curWeaponType; } }
    public Color BaseColor { get { return _baseColor; } }

    public override void Init()
    {
        base.Init();

        Define.ObjectType playerType = (Define.ObjectType)Managers.Game.PlayerId;
        Data.PlayerStat statData = Managers.Data.PlayerStatDict[playerType.ToString()];

        _maxHp = statData.maxHp;
        _hp = _maxHp;
        _speed = statData.speed;
        _curWeaponType = (Define.WeaponType)statData.weaponId;

        _baseColor = ConvertColor(statData.baseColor);
        Managers.Game.LoadWeapon(_curWeaponType, transform);
    }

    private Color ConvertColor(string colorString)
    {
        Color color = Color.white;
        switch (colorString)
        {
            case "red":
                color = new Color(1f, 0.3f, 0.3f);
                break;
            case "green":
                color = new Color(0.3f, 1f, 0.3f);
                break;
            case "blue":
                color = new Color(0.3f, 0.3f, 1f);
                break;
            case "white":
                color = Color.white;
                break;
        }

        return color;
    }
}
