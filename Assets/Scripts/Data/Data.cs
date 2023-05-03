using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Stat
    {
        public int id;
        public string name;
        public int power;
        public int maxHp;
        public int speed;
        public int weaponId;
    }

    public class MonsterStat
    {
        public int id;
        public string name;
        public int maxHp;
        public int speed;
        public float attackRange;
        public float attackSpeed;
    }

    public class Weapon
    {
        public int id;
        public float power;
        public int maxAmmo;
        public int fullLoadAmmo;
        public float bulletSpeed;
        public float fireSpeed;
        public string bulletResourcePath;
    }
}