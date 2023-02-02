using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Stat
    {
        public int id;
        public string name;
        public int maxHp;
        public int power;
        public int speed;
        public int weaponId;
    }

    [Serializable]
    public class Weapon
    {
        public int id;
        public string name;
        public float power;
        public int maxAmmo;
        public int fullLoadAmmo;
        public string bulletResourcePath;
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
    public class WeaponData
    {
        public List<Weapon> weapons = new List<Weapon>();

        public Dictionary<string, Weapon> MakeDict()
        {
            Dictionary<string, Weapon> statDict = new Dictionary<string, Weapon>();

            for (int i = 0; i < weapons.Count; i++)
            {
                statDict[weapons[i].name] = weapons[i];
            }

            return statDict;
        }
    }
}