using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, Data.MonsterStat> monsterStatDict;
    public Dictionary<string, Data.Stat> playerStatDict;
    public Dictionary<string, Data.Weapon> weaponDict;
    public Define.RoomData roomData;

    public void Init()
    {
        monsterStatDict = Util.LoadJsonDict<string, Data.MonsterStat>("Data/MonsterStatData");
        playerStatDict = Util.LoadJsonDict<string, Data.Stat>("Data/PlayerStatData");
        weaponDict = Util.LoadJsonDict<string, Data.Weapon>("Data/WeaponData");
        roomData = Util.LoadJson<Define.RoomData>("Data/RoomData");
    }
}
