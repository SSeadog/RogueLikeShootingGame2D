using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] Define.ObjectType type;

    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _power;

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public float Power { get { return _power;} }

    void Start()
    {
        Data.Stat stat = Manager.Data.statDict[type.ToString()];

        _hp = stat.maxHp;
        _speed = stat.speed;
        _power = stat.power;
    }
}
