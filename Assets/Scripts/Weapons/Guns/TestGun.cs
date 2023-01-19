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
        // TODO
        // BulletControll ���ӿ�����Ʈ�� �ΰ� �Ŵ����� ����صα�?

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, transform.rotation, _bulletRoot.transform);

        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 fireVec = (worldMousePoint - _firePos.position).normalized;

        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
    }

    public override void Reload()
    {

    }
}
