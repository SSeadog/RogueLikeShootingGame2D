using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] Vector3 bossSpawnPoint = new Vector3(0, 0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnBossMonster();
        }
    }

    void SpawnBossMonster()
    {
        GameObject bossEnemyOriginal = Resources.Load<GameObject>("Prefabs/Characters/BossEnemy");

        GameObject bossEnemy = Instantiate(bossEnemyOriginal, bossSpawnPoint, Quaternion.identity);
        bossEnemy.name = bossEnemyOriginal.name;
    }
}
