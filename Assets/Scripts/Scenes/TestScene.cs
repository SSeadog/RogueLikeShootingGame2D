using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        GameObject playerOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestPlayer");
        GameObject enemyOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestBombEnemy");
        GameObject cameraOriginal = Resources.Load<GameObject>("Prefabs/Main Camera");

        GameObject player = Instantiate(playerOriginal);
        GameObject enemy = Instantiate(enemyOriginal, new Vector3(-10f, 0f, 0f), Quaternion.identity);
        GameObject camera = Instantiate(cameraOriginal);

        player.name = playerOriginal.name;
        enemy.name = enemyOriginal.name;
        camera.name = cameraOriginal.name;

        player.GetComponent<Stat>().Init();
        enemy.GetComponent<Stat>().Init();

        camera.GetComponent<CameraController>().Init(player);
    }
}
