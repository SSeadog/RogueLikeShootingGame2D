using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponBase
{
    int _bulletCount = 3;
    int _fireTime = 2;
    float _gap = 10f;

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
        StartCoroutine(CoGenerateBullets());
    }

    IEnumerator CoGenerateBullets()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = (worldMousePoint - _firePos.position).normalized;

        float initRotDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - _gap * ((float)(_bulletCount - 1) / 2);

        for (int j = 0; j < _fireTime; j++)
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                float tempRotDeg = initRotDeg + _gap * i;
                
                float rotRad = tempRotDeg * Mathf.Deg2Rad;

                Vector3 fireVec = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad), 0).normalized;

                GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, transform.rotation, _bulletRoot.transform);
                instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _power);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Reload()
    {
        base.Reload();
    }
}
