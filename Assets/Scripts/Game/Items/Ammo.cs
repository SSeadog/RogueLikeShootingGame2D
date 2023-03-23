using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // ItemBase 두고
    // SellItemBase를 만들어서 Item을 갖고 있도록 하고
    // Trigger Enter 시 가격을 UI로 띄워주고
    // 키 누르면 아이템을 획득시키는 방식으로 구현

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
}
