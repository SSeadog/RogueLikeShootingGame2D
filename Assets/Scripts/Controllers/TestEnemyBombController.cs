using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyBombController : EnemyControllerBase
{
    // Todo
    // 폭발 후 사망, 총에 맞아 사망 시 폭발 구현
    // 데미지 주는 거 구현

    float _jumpPower = 100f;
    float _explodePower = 100f;

    Rigidbody2D _rb;

    public override void Init()
    {
        base.Init();

        _attackSpeed = 3f;
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Attack()
    {
        base.Attack();

        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        // 도약 준비
        yield return new WaitForSeconds(0.2f);

        Vector3 dir = (_target.transform.position - transform.position).normalized;
        // 도약
        _rb.AddForce(dir * _jumpPower);
        yield return new WaitForSeconds(0.8f);
        
        _rb.velocity = Vector2.zero;

        // 폭발
        Explode();
    }

    void Explode()
    {
        // 반경 안이면 데미지 + 밀어내기
        // 반경 밖이면 일정 범위 밀어내기

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);

        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") == false)
                continue;

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log(rb.name);

                Vector3 explodeVec = new Vector3(rb.position.x, rb.position.y, 0) - transform.position;
                rb.AddForce(explodeVec.normalized * _explodePower, ForceMode2D.Impulse);
            }
        }
    }

    protected override void Die()
    {
        base.Die();
    }
}
