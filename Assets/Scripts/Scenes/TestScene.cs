using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        GameObject playerOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestPlayer");
        GameObject enemyOriginal1 = Resources.Load<GameObject>("Prefabs/Characters/TestBombEnemy");
        GameObject enemyOriginal2 = Resources.Load<GameObject>("Prefabs/Characters/TestEnemy");
        GameObject cameraOriginal = Resources.Load<GameObject>("Prefabs/Main Camera");

        GameObject player = Instantiate(playerOriginal);
        player.name = playerOriginal.name;
        player.GetComponent<Stat>().Init();

        GameObject camera = Instantiate(cameraOriginal);
        camera.name = cameraOriginal.name;
        camera.GetComponent<CameraController>().Init(player);

        //for (int i = 0; i < 5; i++)
        //{
        //    Vector3 spawnPos = new Vector3(Random.Range(-1f, 1f) * 10f, Random.Range(-1f, 1f) * 10f, 0f);

        //    GameObject enemy = Instantiate(enemyOriginal1, spawnPos, Quaternion.identity);
        //    enemy.name = enemyOriginal1.name;
        //    enemy.GetComponent<Stat>().Init();
        //}

        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-1f, 1f) * 10f, Random.Range(-1f, 1f) * 10f, 0f);

            GameObject enemy = Instantiate(enemyOriginal2, spawnPos, Quaternion.identity);
            enemy.name = enemyOriginal2.name;
            enemy.GetComponent<Stat>().Init();
        }
    }
}
