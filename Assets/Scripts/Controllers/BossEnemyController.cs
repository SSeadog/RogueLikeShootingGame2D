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
        Arc,
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
                return _stat.AttackSpeed;
            case EAttackType.Tornado:
                StartCoroutine(TornadoFire());
                return _stat.AttackSpeed * 3f;
            case EAttackType.Arc:
                StartCoroutine(ArcFire());
                return _stat.AttackSpeed * 2f;
            default:
                return _stat.AttackSpeed;
        }
    }

    void BasicFire()
    {
        Vector3 fireVec = (_target.transform.position - transform.position).normalized;
        float rotDeg = Mathf.Atan2(fireVec.y, fireVec.x) * Mathf.Rad2Deg;

        GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/EnemyBullet", _bulletRoot.transform);
        instance.transform.position = transform.position + fireVec;
        instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad)) * _firePower);
    }

    IEnumerator TornadoFire()
    {
        Vector3 initFireVec = (_target.transform.position - transform.position).normalized;
        float initRotDeg = Mathf.Atan2(initFireVec.y, initFireVec.x) * Mathf.Rad2Deg;

        int bulletCount = 60;
        int curBulletCount = 0;

        while (curBulletCount < bulletCount)
        {
            float rotDeg = initRotDeg + curBulletCount * 10f;
            Vector2 fireVec = new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad));
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/EnemyBullet", _bulletRoot.transform);
            instance.transform.position = transform.position + new Vector3(fireVec.x, fireVec.y, 0f) * 2f;
            instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
            instance.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
            curBulletCount++;

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator ArcFire()
    {
        Vector3 initFireVec = (_target.transform.position - transform.position).normalized;
        float initRotDeg = Mathf.Atan2(initFireVec.y, initFireVec.x) * Mathf.Rad2Deg;

        int bulletCount = 96;
        int curBulletCount = 0;

        while (curBulletCount < bulletCount)
        {
            int fireOneTimeCount = 16;
            for (int i = 0; i < fireOneTimeCount; i++)
            {
                float rotDeg = initRotDeg + (i - fireOneTimeCount / 2 - 1) * 10f;
                Vector2 fireVec = new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad));
                GameObject instance = Managers.Resource.Instantiate("Prefabs/Weapons/EnemyBullet", _bulletRoot.transform);
                instance.transform.position = transform.position + new Vector3(fireVec.x, fireVec.y, 0f) * 2f;
                instance.transform.rotation = Quaternion.AngleAxis(rotDeg - 90, Vector3.forward);
                instance.GetComponent<Rigidbody2D>().AddForce(fireVec * _firePower);
                curBulletCount++;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void OnDamaged()
    {
        base.OnDamaged();

        BossInfoPanel bossInfoUi = Managers.Ui.GetUI<BossInfoPanel>();
        bossInfoUi.SetBossName(_stat.name);
        bossInfoUi.SetBossHpBar(_stat.Hp / _stat.MaxHp);
    }

    protected override void OnDead()
    {
        base.OnDead();
        MainEndState state = new MainEndState();
        state.IsWin = true;
        Managers.Game.SetState(state);
    }
}
