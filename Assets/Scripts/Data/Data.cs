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
    public class StatData
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<string, Data.Stat> MakeDict()
        {
            Dictionary<string, Data.Stat> statDict = new Dictionary<string, Data.Stat>();

            for (int i = 0; i < stats.Count; i++)
            {
                statDict[stats[i].name] = stats[i];
            }

            return statDict;
        }
    }
}