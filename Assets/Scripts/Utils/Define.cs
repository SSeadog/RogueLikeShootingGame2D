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
        Key,
        GunItem
    }
}
