using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    [Serializable]
    public class Stat
    {
        public string name;
        public int maxHp;
        public int power;
        public int speed;
    }

    [Serializable]
    public class PlayerStat
    {
        public int id;
        public string name;
        public int maxHp;
        public int power;
        public int speed;
    }

    [Serializable]
    public class StatData
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<string, Stat> MakeDict()
        {
            Dictionary<string, Stat> statDict = new Dictionary<string, Stat>();

            for (int i = 0; i < stats.Count; i++)
            {
                statDict[stats[i].name] = stats[i];
            }

            return statDict;
        }
    }

    [Serializable]
    public class PlayerStatData
    {
        public List<PlayerStat> playerStats = new List<PlayerStat>();

        public Dictionary<string, PlayerStat> MakeDict()
        {
            Dictionary<string, PlayerStat> statDict = new Dictionary<string, PlayerStat>();

            for (int i = 0; i < playerStats.Count; i++)
            {
                statDict[playerStats[i].name] = playerStats[i];
            }

            return statDict;
        }
    }

    //[Serializable]
    //public class GStatData<T> where T : BaseStat
    //{
    //    public List<T> stats = new List<T>();

    //    public Dictionary<string, T> MakeDict()
    //    {
    //        Dictionary<string, T> statDict = new Dictionary<string, T>();

    //        for (int i = 0; i < stats.Count; i++)
    //        {
    //            statDict[stats[i].name] = stats[i];
    //        }

    //        return statDict;
    //    }
    //}
}