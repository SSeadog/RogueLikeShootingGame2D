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
        // 각자 총에 맞는 총알을 여러개 둔다면. 총알 로드하도록 수정 필요
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/TestBullet");
    }

    public override void Fire()
    {
        // TODO
        // BulletControll 게임오브젝트를 두고 매니저에 등록해두기?

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
