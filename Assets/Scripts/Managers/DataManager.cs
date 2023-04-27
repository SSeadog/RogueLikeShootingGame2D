using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Data.MonsterStat> _monsterStatDict;
    Dictionary<string, Data.Stat> _playerStatDict;
    Dictionary<string, Data.Weapon> _weaponDict;
    Define.RoomData _roomData;

    public Dictionary<string, Data.MonsterStat> MonsterStatDict { get { return _monsterStatDict; } }
    public Dictionary<string, Data.Stat> PlayerStatDict { get { return _playerStatDict; } }
    public Dictionary<string, Data.Weapon> WeaponDict { get { return _weaponDict; } }
    public Define.RoomData RoomData { get { return _roomData; } }

    public void Init()
    {
        _monsterStatDict = Util.LoadJsonDict<string, Data.MonsterStat>("Data/MonsterStatData");
        _playerStatDict = Util.LoadJsonDict<string, Data.Stat>("Data/PlayerStatData");
        _weaponDict = Util.LoadJsonDict<string, Data.Weapon>("Data/WeaponData");
        _roomData = Util.LoadJson<Define.RoomData>(Application.persistentDataPath + "/TestRoomData.json");
    }
}
