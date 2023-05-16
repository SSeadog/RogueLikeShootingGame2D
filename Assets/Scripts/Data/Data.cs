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
        public float reloadSpeed;
        public string bulletResourcePath;
        public string thumbnailPath;
    }

    public class TriggerInfo
    {
        public float posX;
        public float posY;
        public float sizeX;
        public float sizeY;
    }

    public class SpawnInfo
    {
        public Define.ObjectType type;
        public float posX;
        public float posY;
    }

    public class DoorInfo
    {
        public Define.ObjectType type;
        public float posX;
        public float posY;
        public bool isClose;
    }

    public class RoomData
    {
        public string name;
        public string map;
        public List<Room> rooms;
    }
}