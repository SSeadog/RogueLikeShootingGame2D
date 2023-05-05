using System.Collections;
using UnityEngine;

public class BombEnemyController : EnemyControllerBase
{
    // Todo
    // ���� �� ���, �ѿ� �¾� ��� �� ���� ����
    // ������ �ִ� �� ����

    private float _explodeRange = 5f;
    private float _damgeRange = 3f;
    private float _jumpPower = 200f;
    private float _explodePower = 20f;

    private Collider2D _collider;
    private Rigidbody2D _rb;

    public override void Init()
    {
        base.Init();
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override float Attack()
    {
        StartCoroutine(CoAttack());
        return _stat.AttackSpeed;
    }

    IEnumerator CoAttack()
    {
        // ���� �غ�
        Vector3 dir = (_target.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(0.2f);

        // ����
        _collider.enabled = false;
        _rb.AddForce(dir * _jumpPower);

        // ���� ���������� �� ����
        ChangeColor(new Color(255f, 0f, 0f));
        float colorChangeTime = 0.4f;
        float colorChangeTimer = 0f;
        while (colorChangeTimer < colorChangeTime)
        {
            colorChangeTimer += Time.deltaTime;
            Color color = new Color(255f, (1 - colorChangeTimer / colorChangeTime) * 144f, (1 - colorChangeTimer / colorChangeTime) * 47f);
            ChangeColor(color);
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);

        _rb.velocity = Vector2.zero;

        // ����
        Explode();
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explodeRange);

        
        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") == true || collider.CompareTag("PlayerBullet") == true || collider.CompareTag("EnemyBullet") == true)
                continue;

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector3 explodeVec = new Vector3(rb.position.x, rb.position.y, 0) - transform.position;
                float dist = explodeVec.magnitude;
                // ������ �ݰ� ���̸� ������ �ֱ�
                if (dist < _damgeRange)
                    rb.gameObject.GetComponent<PlayerStat>()?.GetDamaged(1f);

                // ���� �ݰ� ���̸� ���� ���� �о��
                if (dist < _explodeRange)
                    rb.AddForce(explodeVec.normalized * _explodePower * Mathf.Clamp(_explodeRange - dist, 1f, _explodeRange - 1f), ForceMode2D.Impulse);
            }
        }

        // ������ ����ϱ�
        _stat.GetDamaged(_stat.Hp);
    }

    protected override void OnDead()
    {
        base.OnDead();
    }
}
