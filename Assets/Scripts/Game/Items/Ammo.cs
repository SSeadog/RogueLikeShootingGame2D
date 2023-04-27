using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : ItemBase
{

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어의 탄약 증가
        }
    }

    public override void Effect()
    {
        Debug.Log("Ammo획득 구현 필요!");
    }
}
