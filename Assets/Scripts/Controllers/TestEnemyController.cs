using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : EnemyControllerBase
{
    float _firePower;
    GameObject _bullet;

    public override void Init()
    {
        base.Init();

        _firePower = 0.1f;
        _bullet = Resources.Load<GameObject>("Prefabs/Weapons/TestEnemyBullet");
    }

    protected override void Attack()
    {
        // ���� �ӵ��� �ִ� ���
        // 1. Ÿ�̸� ����
        // 2. Coroutine

        Vector3 fireVec = (_target.transform.position - transform.position).normalized;
        float rotDeg = Mathf.Atan2(fireVec.y, fireVec.x) * Mathf.Rad2Deg;

        GameObject instance = Instantiate(_bullet, transform.position + fireVec, Quaternion.AngleAxis(rotDeg - 90, Vector3.forward), _bulletRoot.transform);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad)) * _firePower);

        base.Attack();
    }
}
