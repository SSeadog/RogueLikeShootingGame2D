using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, Data.Stat> statDict;
    public Dictionary<string, Data.PlayerStat> playerStatDict;

    public void Init()
    {
        statDict = Loader.LoadJson<Data.StatData>("Data/StatData").MakeDict();
        playerStatDict = Loader.LoadJson<Data.PlayerStatData>("Data/PlayerStatData").MakeDict();
    }
}
