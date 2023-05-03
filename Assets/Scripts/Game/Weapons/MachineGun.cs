using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBase
{
    float _rebound;
    float _maxRebound = 0.3f;
    public override void Init()
    {
        base.Init();
    }

    public override void LoadBulletResource()
    {
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/TestPlayerBullet");
    }

    public override void FireBullets()
    {
        float mouserot = GetMouseRotDeg() + Random.Range(-_rebound, _rebound);
        Vector3 fireVec = new Vector3(Mathf.Cos(mouserot), Mathf.Sin(mouserot), 0).normalized;

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, Quaternion.AngleAxis(mouserot * Mathf.Rad2Deg + 90, Vector3.forward), _bulletRoot.transform);
        instanceBullet.GetComponent<Bullet>().Power = Power;
        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _bulletSpeed);

        if (_rebound < _maxRebound)
            _rebound += 0.15f;
    }

    public override void Reload()
    {
        base.Reload();
    }

    protected override void ReduceRebound()
    {
        if (_rebound > 0f)
        {
            _rebound -= Time.deltaTime;
        }
    }
}
