using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunBase
{
    public override void Init()
    {
        base.Init();

    }

    public override void LoadBulletResource()
    {
        // ���� �ѿ� �´� �Ѿ��� ������ �дٸ�. �Ѿ� �ε��ϵ��� ���� �ʿ�
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/TestBullet");
    }

    public override void Fire()
    {
        Vector2 fireVec = Vector2.up;

        Quaternion rotation = transform.rotation;

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, rotation);

        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
    }

    public override void Reload()
    {

    }
}
