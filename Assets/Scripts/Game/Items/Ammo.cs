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
            // �÷��̾��� ź�� ����
        }
    }

    public override void Effect()
    {
        Debug.Log("Ammoȹ�� ���� �ʿ�!");
    }
}
