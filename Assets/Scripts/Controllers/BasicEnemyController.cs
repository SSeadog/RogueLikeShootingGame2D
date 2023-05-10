using UnityEngine;

public class BasicEnemyController : EnemyControllerBase
{
    private float _firePower;
    private GameObject _bullet;

    public override void Init()
    {
        base.Init();

        _firePower = 0.1f;
        _bullet = Managers.Resource.Load("Prefabs/Weapons/EnemyBullet");
    }

    public override float Attack()
    {
        Vector3 fireVec = (_target.transform.position - transform.position).normalized;
        float rotDeg = Mathf.Atan2(fireVec.y, fireVec.x) * Mathf.Rad2Deg;

        GameObject instance = Instantiate(_bullet, transform.position + fireVec, Quaternion.AngleAxis(rotDeg - 90, Vector3.forward), _bulletRoot.transform);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotDeg * Mathf.Deg2Rad), Mathf.Sin(rotDeg * Mathf.Deg2Rad)) * _firePower);

        return _stat.AttackSpeed;
    }
}