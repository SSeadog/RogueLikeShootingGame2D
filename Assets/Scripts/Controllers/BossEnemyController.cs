using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyControllerBase
{
    float _firePower;

    public override void Init()
    {
        base.Init();

        _firePower = 0.1f;
    }

    public override void Attack()
    {
        StartCoroutine(TornadeFire());
    }

    void BasicFire()
    {
        Vector3 fireVec = (target.transform.position - transform.position).normalized;
        float rotDeg = Mathf.Atan2(fireVec.y, fireVec.x) * Mathf.Rad2Deg;

        GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/TestEnemyBullet", _bulletRoot.transform);
        instance.transform.position = transform.position + fireVec;
        instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad)) * _firePower);
    }

    IEnumerator TornadeFire()
    {
        Vector3 fireVec = (target.transform.position - transform.position).normalized;
        float initRotDeg = Mathf.Atan2(fireVec.y, fireVec.x) * Mathf.Rad2Deg;

        int bulletCount = 40;
        int curBulletCount = 0;

        while (curBulletCount < bulletCount)
        {
            float rotDeg = initRotDeg + curBulletCount * 5f;
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/TestEnemyBullet", _bulletRoot.transform);
            instance.transform.position = transform.position + fireVec;
            instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad)) * _firePower);
            curBulletCount++;

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        Managers.Game.SetState(new MainEndState());
    }
}
