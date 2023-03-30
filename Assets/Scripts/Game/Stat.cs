using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour
{
    [SerializeField] Define.ObjectType type;

    [SerializeField] private float _hp;
    [SerializeField] private float _speed;

    [SerializeField] Define.WeaponType curWeaponType;

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public Define.WeaponType CurWeaponType { get { return curWeaponType; } }

    public UnityAction onGetDamagedAction;
    public UnityAction onDeadAction;

    public virtual void Init()
    {

        if ((int)type > (int)Define.ObjectType.Monster)
        {
            Data.Stat statData = Managers.Data.monsterStatDict[type.ToString()];

            _hp = statData.maxHp;
            _speed = statData.speed;
        }
        else
        {
            Define.ObjectType playerType = (Define.ObjectType)Managers.Game.PlayerId;
            Data.Stat statData = Managers.Data.playerStatDict[playerType.ToString()];

            _hp = statData.maxHp;
            _speed = statData.speed;

            curWeaponType = (Define.WeaponType)statData.weaponId;
            GameObject weapon = Managers.Resource.Instantiate("Prefabs/Weapons/" + CurWeaponType.ToString(), transform);
            Managers.Game.playerWeaponList.Add(weapon.GetComponent<WeaponBase>());
            weapon.SetActive(false);
        }
    }

    public void GetDamaged(float damage)
    {
        _hp -= damage;
        if (_hp > 0)
        {
            onGetDamagedAction?.Invoke();
        }
        else
        {
            _hp = 0;
            Die();
        }
    }

    void Die()
    {
        onDeadAction?.Invoke();
    }
}
