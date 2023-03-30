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

        Managers.Game.SetState(new MainState());

        GameObject playerOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestPlayer");
        GameObject cameraOriginal = Resources.Load<GameObject>("Prefabs/Main Camera");

        GameObject player = Instantiate(playerOriginal);
        player.name = playerOriginal.name;

        GameObject camera = Instantiate(cameraOriginal);
        camera.name = cameraOriginal.name;
        camera.GetComponent<CameraController>().Init(player);

        // Room Setting. 파일 로드로 변경 예정
        GameState gameState = Managers.Game.GetState();

        Managers.Game.gold = 0;
        Managers.Game.key = 0;
        Managers.Game.grenade = 2;

        if (gameState is MainState)
        {
            MainState mainState = (MainState)gameState;

            Room roomA = new Room(0);
            mainState.roomController.Add(roomA);

            Room roomB = new Room(1);
            mainState.roomController.Add(roomB);
            {
                Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
                spawnInfo.type = Define.ObjectType.TestEnemy;
                spawnInfo.spawnPoint = new Vector3(53f, 1f, 0f);
                roomB.AddSpawnInfo(spawnInfo);
            }

            Room roomBoss = new Room(4);
            mainState.roomController.Add(roomBoss);
            {
                Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
                spawnInfo.type = Define.ObjectType.BossEnemy;
                spawnInfo.spawnPoint = new Vector3(8f, 75f, 0f);
                roomBoss.AddSpawnInfo(spawnInfo);
            }
        }
        
    }

    public override void Clear()
    {
        Debug.Log("MainScene Clear()");
    }
}
