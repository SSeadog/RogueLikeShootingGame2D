using UnityEngine;

public class Food : ItemBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� ü�� ����
        }
    }

    public override void Effect()
    {
        Debug.Log("player Hp 2ĭ ���� ���� �ʿ�!");
    }
}
