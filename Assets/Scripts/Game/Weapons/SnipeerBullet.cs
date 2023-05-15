using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeerBullet : Bullet
{
    void Start()
    {
        StartCoroutine(CoDestroy());
    }

    IEnumerator CoDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    protected override void OnTriggerEnterAction(Collider2D collision)
    {
        // �������� �Ѿ��� Ʈ���� ������ �ʿ� �������� �� �������� ���
    }
}
