using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Data.MonsterStat> _monsterStatDict;
    Dictionary<string, Data.PlayerStat> _playerStatDict;
    Dictionary<string, Data.Weapon> _weaponDict;

    public Dictionary<string, Data.MonsterStat> MonsterStatDict { get { return _monsterStatDict; } }
    public Dictionary<string, Data.PlayerStat> PlayerStatDict { get { return _playerStatDict; } }
    public Dictionary<string, Data.Weapon> WeaponDict { get { return _weaponDict; } }
    public Data.RoomData RoomData { get { return Util.LoadJson<Data.RoomData>(Application.persistentDataPath + "/RoomData.json"); } }

    public void Init()
    {
        _monsterStatDict = Util.LoadJsonDict<string, Data.MonsterStat>("Data/MonsterStatData");
        _playerStatDict = Util.LoadJsonDict<string, Data.PlayerStat>("Data/PlayerStatData");
        _weaponDict = Util.LoadJsonDict<string, Data.Weapon>("Data/WeaponData");

        // 처음 실행 시 persistentDataPath에 RoomData파일을 넣어주는 코드
        if (File.Exists(Application.persistentDataPath + "/RoomData.json") == false)
        {
            Data.RoomData roomData = Util.LoadJson<Data.RoomData>("Data/RoomData");
            string jsonData = Util.ToJson(roomData);
            using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/RoomData.json"))
            {
                sw.Write(jsonData);
            }
        }
    }

    public void Clear()
    {
        _monsterStatDict = null;
        _playerStatDict = null;
        _weaponDict = null;
    }
}
