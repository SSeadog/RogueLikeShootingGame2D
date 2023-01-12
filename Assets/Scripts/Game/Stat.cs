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
        Dictionary<string, Data.Stat> statDict = Loader.LoadJson<Data.StatData>("Data/StatData").MakeDict();
        Data.Stat stat = statDict[type.ToString()];

        _hp = stat.maxHp;
        _speed = stat.speed;
        _power = stat.power;
    }
}
