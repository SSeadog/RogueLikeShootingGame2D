using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBombEnemyController : EnemyControllerBase
{
    // Todo
    // ���� �� ���, �ѿ� �¾� ��� �� ���� ����
    // ������ �ִ� �� ����

    float _explodeRange = 5f;
    float _jumpPower = 200f;
    float _explodePower = 50f;

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
        // ���� �غ�
        yield return new WaitForSeconds(0.2f);

        Vector3 dir = (_target.transform.position - transform.position).normalized;
        // ����
        _rb.AddForce(dir * _jumpPower);
        yield return new WaitForSeconds(0.8f);
        
        _rb.velocity = Vector2.zero;

        // ����
        Explode();
    }

    void Explode()
    {
        // �ݰ� ���̸� ������ + �о��
        // �ݰ� ���̸� ���� ���� �о��
        // �Ÿ��� ���� �о�� �� �����ϱ�

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
                rb.AddForce(explodeVec.normalized * _explodePower * Mathf.Max(0f, _explodeRange - dist), ForceMode2D.Impulse);
            }
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
    }
}
