using UnityEngine;

public class Food : ItemBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 체력 증가
        }
    }

    public override void Effect(Stat stat)
    {
        stat.RecoverHp(2);
    }
}
