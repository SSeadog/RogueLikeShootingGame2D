using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyControllerBase
{
    float _firePower;

    enum EAttackType
    {
        None = 0,
        Basic,
        Tornado,
        Max
    }

    public override void Init()
    {
        base.Init();

        _firePower = 0.1f;
    }

    public override float Attack()
    {
        EAttackType randAttack = (EAttackType)Random.Range((int)EAttackType.Basic, (int)EAttackType.Max);

        switch (randAttack)
        {
            case EAttackType.Basic:
                BasicFire();
                return attackSpeed;
            case EAttackType.Tornado:
                StartCoroutine(TornadoFire());
                return attackSpeed * 3f;
            default:
                return attackSpeed;
        }
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

    IEnumerator TornadoFire()
    {
        Vector3 initFireVec = (target.transform.position - transform.position).normalized;
        float initRotDeg = Mathf.Atan2(initFireVec.y, initFireVec.x) * Mathf.Rad2Deg;

        int bulletCount = 40;
        int curBulletCount = 0;

        while (curBulletCount < bulletCount)
        {
            float rotDeg = initRotDeg + curBulletCount * 10f;
            Vector2 fireVec = new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad));
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/TestEnemyBullet", _bulletRoot.transform);
            instance.transform.position = transform.position + new Vector3(fireVec.x, fireVec.y, 0f) * 2f;
            instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
            instance.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
            curBulletCount++;

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDamaged()
    {
        base.OnDamaged();

        BossInfoPanel bossInfoUi = Managers.Ui.GetUI<BossInfoPanel>();
        bossInfoUi.SetBossName(stat.name);
        bossInfoUi.SetBossHpBar(stat.Hp / stat.MaxHp);
    }

    protected override void OnDead()
    {
        base.OnDead();
        Managers.Game.SetState(new MainEndState());
    }
}
