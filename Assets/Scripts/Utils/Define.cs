using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ObjectType
    {
        Character = 0,
        Apple,
        Banana,
        Chocolate,
        DarkChocolate,
        EnergyBar,
        Food,
        Monster = 10000,
        Hyena,
        BasicEnemy,
        BombEnemy,
        BossEnemy,
        BasicEnemyMaking,
        BombEnemyMaking,
        BossEnemyMaking,
        Object = 20000,
        Room,
        Store,
        DoorHorizontal,
        DoorVertical,
        RoomTrigger,
        RoomMaking,
        RoomTriggerMaking,
        DoorHorizontalMaking,
        DoorVerticalMaking,
        StoreMaking,
        ObjectEnd
    }

    public enum WeaponType
    {
        None,
        MachineGun,
        ShotGun,
        SniperRifle
    }

    public enum ItemType
    {
        None,
        Food,
        Ammo,
        Key
    }

    public class TriggerInfo
    {
        public float posX;
        public float posY;
        public float sizeX;
        public float sizeY;
        public Vector3 GetPosition() { return new Vector3(posX, posY, 0); }
    }

    public class SpawnInfo
    {
        public Define.ObjectType type;
        public float posX;
        public float posY;
        public Vector3 GetPosition() { return new Vector3(posX, posY, 0); }
    }

    public class DoorInfo
    {
        public Define.ObjectType type;
        public float posX;
        public float posY;
        public bool isClose;
        public Vector3 GetPosition() { return new Vector3(posX, posY, 0); }
    }

    public class RoomData
    {
        public string name;
        public string map;
        public List<Room> rooms;
    }
}
