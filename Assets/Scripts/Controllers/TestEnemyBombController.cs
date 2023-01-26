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
        // Ÿ�� �������� ���� ���ƿͼ�
        // ���� �ڿ� ����
        // �ݰ� ���̸� ������ + �о��
        // �ݰ� ���̸� ���� ���� �о��
        StartCoroutine(CoAttack());
    }

    IEnumerator CoAttack()
    {
        Debug.Log("����!!!");
        // ���� �غ�
        yield return new WaitForSeconds(0.2f);

        Vector3 dir = (_target.transform.position - transform.position).normalized;
        // ����
        _rb.AddForce(dir * _jumpPower);
        yield return new WaitForSeconds(0.8f);
        
        _rb.velocity = Vector2.zero;

        Explode();
    }

    void Explode()
    {
        Debug.Log("����!");

    }

    protected override void Die()
    {
        base.Die();

        Explode();
    }
}
