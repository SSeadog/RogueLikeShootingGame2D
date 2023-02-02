using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyBombController : EnemyControllerBase
{
    // Todo
    // ���� �� ���, �ѿ� �¾� ��� �� ���� ����
    // ������ �ִ� �� ����

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
