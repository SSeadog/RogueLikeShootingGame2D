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

    public override void Effect(Stat stat)
    {
        stat.GetComponent<PlayerController>().CurWeapon.CurAmmo += 70;
    }
}
