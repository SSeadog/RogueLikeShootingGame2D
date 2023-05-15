using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : WeaponBase
{
    public override void Init()
    {
        base.Init();
        _reboundDelta = 0f;
    }

    public override void LoadBulletResource()
    {
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/PlayerSniperBullet");
    }

    public override void FireBullets()
    {
        // 0.1초 정도 생성 후 삭제
        float mouserot = GetMouseRotRad() + GetCurRebound();
        Vector2 fireVec = new Vector2(Mathf.Cos(mouserot), Mathf.Sin(mouserot));

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, Quaternion.AngleAxis(mouserot * Mathf.Rad2Deg - 90, Vector3.forward), _bulletRoot.transform);
        instanceBullet.GetComponent<Bullet>().Power = Power;
        instanceBullet.transform.position += new Vector3(fireVec.x * 7f, fireVec.y * 7f, 0f);
    }
}
