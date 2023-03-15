using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ObjectType
    {
        Apple = 1,
        Banana,
        Chocolate,
        DarkChocolate,
        EnergyBar,
        Food,
        Monster = 10001,
        Hyena,
        Bomb,
        BossEnemy,
        TestEnemy,
    }

    public enum WeaponType
    {
        None,
        TestGun,
        ShotGun
    }

    public class SpawnInfo
    {
        public Define.ObjectType type;
        public Vector3 spawnPoint;
    }

}
