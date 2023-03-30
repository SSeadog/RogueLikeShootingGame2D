using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : ItemBase
{
    public override void Effect()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 체력 증가
        }
    }
}
