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
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/TestPlayerBullet");
    }

    public override void GenerateBullets()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = (worldMousePoint - _firePos.position).normalized;

        float rotRad = Mathf.Atan2(dir.y, dir.x);
        Vector3 fireVec = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad), 0).normalized;

        GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, transform.rotation, _bulletRoot.transform);
        instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _power);
    }

    public override void Reload()
    {
        base.Reload();
    }
}
