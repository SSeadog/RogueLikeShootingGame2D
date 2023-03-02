using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, Data.Stat> monsterStatDict;
    public Dictionary<string, Data.Stat> playerStatDict;
    public Dictionary<string, Data.Weapon> weaponDict;

    public void Init()
    {
        monsterStatDict = Util.LoadJson<Data.StatData>("Data/MonsterStatData").MakeDict();
        playerStatDict = Util.LoadJson<Data.StatData>("Data/PlayerStatData").MakeDict();
        weaponDict = Util.LoadJson<Data.WeaponData>("Data/WeaponData").MakeDict();
    }
}
