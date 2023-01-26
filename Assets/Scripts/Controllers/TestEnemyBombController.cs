using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyBombController : EnemyControllerBase
{
    float _jumpPower = 5f;

    Rigidbody2D _rb;

    public override void Init()
    {
        base.Init();

        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void Attack()
    {
        // 타겟 방향으로 조금 날아와서
        // 조금 뒤에 폭발
        // 반경 안이면 데미지 + 밀어내기
        // 반경 밖이면 일정 범위 밀어내기
        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        Debug.Log("공격!!!");
        // 도약 준비
        yield return new WaitForSeconds(0.2f);

        Vector3 dir = (_target.transform.position - transform.position).normalized;
        // 도약
        _rb.AddForce(dir * _jumpPower);
        yield return new WaitForSeconds(0.8f);
        
        _rb.velocity = Vector2.zero;

        Explode();
    }

    void Explode()
    {
        Debug.Log("폭발!");

    }

    protected override void Die()
    {
        base.Die();

        Explode();
    }
}
