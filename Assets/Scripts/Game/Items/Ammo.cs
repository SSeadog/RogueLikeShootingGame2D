using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // ItemBase �ΰ�
    // SellItemBase�� ���� Item�� ���� �ֵ��� �ϰ�
    // Trigger Enter �� ������ UI�� ����ְ�
    // Ű ������ �������� ȹ���Ű�� ������� ����

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
}
