using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : WeaponBase
{
    public override void Init()
    {
        base.Init();
    }

    public override void LoadBulletResource()
    {
        // ���� �ѿ� �´� �Ѿ��� ������ �дٸ�. �Ѿ� �ε��ϵ��� ���� �ʿ�
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/TestPlayerBullet");
    }

    public override void FireBullets()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = (worldMousePoint - _firePos.position).normalized;

        float rotRad = Mathf.Atan2(dir.y, dir.x);
        Vector3 fireVec = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad), 0).normalized;

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, Quaternion.AngleAxis(rotRad * Mathf.Rad2Deg + 90, Vector3.forward), _bulletRoot.transform);
        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _bulletSpeed);
    }

    public override void Reload()
    {
        base.Reload();
    }
}
