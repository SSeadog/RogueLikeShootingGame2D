using System.Collections;
using UnityEngine;

public class ShotGun : WeaponBase
{
    private int _bulletCount = 4;
    private int _fireTime = 2;
    private float _gap = 10f;

    public override void Init()
    {
        base.Init();
        _reboundDelta = 0.1f;
    }

    public override void LoadBulletResource()
    {
        // 각자 총에 맞는 총알을 여러개 둔다면. 총알 로드하도록 수정 필요
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/PlayerBullet");
    }

    public override void FireBullets()
    {
        StartCoroutine(CoGenerateBullets());
    }

    // 마우스 방향으로 _fireTime만큼 부채꼴 총알 웨이브를 만듦
    // 각 웨이브의 총알 개수는 이전 개수 + 1
    IEnumerator CoGenerateBullets()
    {
        int tempBulletCount = _bulletCount;
        for (int j = 0; j < _fireTime; j++)
        {
            float initRotDeg = GetMouseRotRad() * Mathf.Rad2Deg - _gap * ((float)(tempBulletCount - 1) / 2);
            for (int i = 0; i < tempBulletCount; i++)
            {
                float tempRotDeg = initRotDeg + _gap * i;
                float rotRad = tempRotDeg * Mathf.Deg2Rad;

                Vector3 fireVec = new Vector3(Mathf.Cos(rotRad), Mathf.Sin(rotRad), 0).normalized;

                GameObject instanceBullet = Instantiate(_bulletOrigin, _firePos.position, Quaternion.AngleAxis(rotRad * Mathf.Rad2Deg + 90, Vector3.forward), _bulletRoot.transform);
                instanceBullet.GetComponent<Bullet>().Power = Power;
                instanceBullet.GetComponent<Rigidbody2D>().AddForce(fireVec * _bulletSpeed);
            }
            tempBulletCount++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public override void Reload()
    {
        base.Reload();
    }
}
