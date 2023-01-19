using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase
{
    int _bulletCount = 3;
    int _fireTime = 2;
    float _gap = 5f;

    public override void Init()
    {
        base.Init();
    }

    public override void LoadBulletResource()
    {
        // ���� �ѿ� �´� �Ѿ��� ������ �дٸ�. �Ѿ� �ε��ϵ��� ���� �ʿ�
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/TestBullet");
    }

    public override void Fire()
    {
        StartCoroutine(CoFire());
    }

    IEnumerator CoFire()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = (worldMousePoint - _firePos.position).normalized;

        float rotDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (_gap * (_bulletCount / 2));

        for (int j = 0; j < _fireTime; j++)
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                float tempRotDeg = rotDeg + _gap * i;
                float rotRad = tempRotDeg * Mathf.Deg2Rad;

                Vector3 fireVec = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad), 0).normalized;

                GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, transform.rotation, _bulletRoot.transform);
                instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Reload()
    {
    }
}
