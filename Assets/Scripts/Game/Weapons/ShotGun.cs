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
        // ���� �ѿ� �´� �Ѿ��� ������ �дٸ�. �Ѿ� �ε��ϵ��� ���� �ʿ�
        _bulletOrigin = Resources.Load<GameObject>("Prefabs/Weapons/PlayerBullet");
    }

    public override void FireBullets()
    {
        StartCoroutine(CoGenerateBullets());
    }

    // ���콺 �������� _fireTime��ŭ ��ä�� �Ѿ� ���̺긦 ����
    // �� ���̺��� �Ѿ� ������ ���� ���� + 1
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
