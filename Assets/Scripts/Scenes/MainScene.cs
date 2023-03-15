using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        // TestCode
        if (Managers.Game.PlayerId == 0)
            Managers.Game.PlayerId = 1;

        GameObject playerOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestPlayer");
        GameObject cameraOriginal = Resources.Load<GameObject>("Prefabs/Main Camera");

        GameObject player = Instantiate(playerOriginal);
        player.name = playerOriginal.name;

        GameObject camera = Instantiate(cameraOriginal);
        camera.name = cameraOriginal.name;
        camera.GetComponent<CameraController>().Init(player);

        // Room Setting
        Room roomA = new Room(0);
        Managers.Game.rooms.Add(roomA);

        Room roomB = new Room(1);
        Managers.Game.rooms.Add(roomB);
        {
            Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
            spawnInfo.type = Define.ObjectType.TestEnemy;
            spawnInfo.spawnPoint = new Vector3(53f, 1f, 0f);
            roomB.AddSpawnInfo(spawnInfo);
        }

        Room roomBoss = new Room(4);
        Managers.Game.rooms.Add(roomBoss);
        {
            Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
            spawnInfo.type = Define.ObjectType.BossEnemy;
            spawnInfo.spawnPoint = new Vector3(8f, 75f, 0f);
            roomBoss.AddSpawnInfo(spawnInfo);
        }
    }

    public override void Clear()
    {
        Debug.Log("MainScene Clear()");
    }
}
