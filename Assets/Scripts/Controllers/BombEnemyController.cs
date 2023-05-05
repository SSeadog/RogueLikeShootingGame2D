using System.Collections;
using UnityEngine;

public class BombEnemyController : EnemyControllerBase
{
    // Todo
    // 폭발 후 사망, 총에 맞아 사망 시 폭발 구현
    // 데미지 주는 거 구현

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
        // 도약 준비
        Vector3 dir = (_target.transform.position - transform.position).normalized;
        yield return new WaitForSeconds(0.2f);

        // 도약
        _collider.enabled = false;
        _rb.AddForce(dir * _jumpPower);

        // 점점 빨개지도록 색 변경
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

        // 폭발
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
                // 데미지 반경 안이면 데미지 주기
                if (dist < _damgeRange)
                    rb.gameObject.GetComponent<PlayerStat>()?.GetDamaged(1f);

                // 폭발 반경 안이면 일정 범위 밀어내기
                if (dist < _explodeRange)
                    rb.AddForce(explodeVec.normalized * _explodePower * Mathf.Clamp(_explodeRange - dist, 1f, _explodeRange - 1f), ForceMode2D.Impulse);
            }
        }

        // 터지면 사망하기
        _stat.GetDamaged(_stat.Hp);
    }

    protected override void OnDead()
    {
        base.OnDead();
    }
}
