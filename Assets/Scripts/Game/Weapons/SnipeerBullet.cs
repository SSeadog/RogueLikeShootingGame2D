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
        // 스나이퍼 총알은 트리거 로직이 필요 없음으로 빈 로직으로 덮어씀
    }
}
