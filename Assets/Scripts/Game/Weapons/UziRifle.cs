using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziRifle : WeaponBase
{
    public override void Init()
    {
        base.Init();
        _reboundDelta = 0.2f;
    }

    public override void LoadBulletResource()
    {
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/PlayerBullet");
    }

    public override void FireBullets()
    {
        float mouserot = GetMouseRotRad() + GetCurRebound();
        Vector3 fireVec = new Vector3(Mathf.Cos(mouserot), Mathf.Sin(mouserot), 0).normalized;

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, Quaternion.AngleAxis(mouserot * Mathf.Rad2Deg + 90, Vector3.forward), _bulletRoot.transform);
        instanceBullet.GetComponent<Bullet>().Power = Power;
        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _bulletSpeed);
    }
}
