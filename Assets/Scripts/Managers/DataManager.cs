using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Data.MonsterStat> _monsterStatDict;
    Dictionary<string, Data.PlayerStat> _playerStatDict;
    Dictionary<string, Data.Weapon> _weaponDict;

    public Dictionary<string, Data.MonsterStat> MonsterStatDict { get { return _monsterStatDict; } }
    public Dictionary<string, Data.PlayerStat> PlayerStatDict { get { return _playerStatDict; } }
    public Dictionary<string, Data.Weapon> WeaponDict { get { return _weaponDict; } }
    public Data.RoomData RoomData { get { return Util.LoadJson<Data.RoomData>(Application.persistentDataPath + "/TestRoomData.json"); } }

    public void Init()
    {
        _monsterStatDict = Util.LoadJsonDict<string, Data.MonsterStat>("Data/MonsterStatData");
        _playerStatDict = Util.LoadJsonDict<string, Data.PlayerStat>("Data/PlayerStatData");
        _weaponDict = Util.LoadJsonDict<string, Data.Weapon>("Data/WeaponData");
    }

    public void Clear()
    {
        _monsterStatDict = null;
        _playerStatDict = null;
        _weaponDict = null;
    }
}
