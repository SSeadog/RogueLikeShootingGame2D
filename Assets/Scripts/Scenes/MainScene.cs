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

        Managers.Game.gold = 0;
        Managers.Game.key = 0;
        Managers.Game.grenade = 2;

        MakeRooms();
        LoadUI();
    }

    void MakeRooms()
    {
        // Room Setting. 파일 로드로 변경 예정
        //{
        //    Room ARoom = new Room("ARoom");
        //    Managers.Game.roomController.Add(ARoom);
        //}

        //{
        //    Room BRoom = new Room("BRoom");
        //    Managers.Game.roomController.Add(BRoom);
        //    Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
        //    spawnInfo.type = Define.ObjectType.TestEnemy;
        //    spawnInfo.spawnPoint = new Vector3(53f, 1f, 0f);
        //    BRoom.AddSpawnInfo(spawnInfo);
        //}

        //{
        //    Room CRoom = new Room("CRoom");
        //    Managers.Game.roomController.Add(CRoom);
        //    Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
        //    spawnInfo.type = Define.ObjectType.DoorHorizontal;
        //    spawnInfo.spawnPoint = new Vector3(3f, 26f, 0f);
        //    CRoom.AddSpawnInfo(spawnInfo);

        //    Define.SpawnInfo spawnInfo1 = new Define.SpawnInfo();
        //    spawnInfo1.type = Define.ObjectType.DoorHorizontal;
        //    spawnInfo1.spawnPoint = new Vector3(12f, 46f, 0f);
        //    CRoom.AddSpawnInfo(spawnInfo1);

        //    Define.SpawnInfo spawnInfo2 = new Define.SpawnInfo();
        //    spawnInfo2.type = Define.ObjectType.DoorVertical;
        //    spawnInfo2.spawnPoint = new Vector3(18f, 35.5f, 0f);
        //    CRoom.AddSpawnInfo(spawnInfo2);
        //}

        //{
        //    Room BossRoom = new Room("BossRoom");
        //    Managers.Game.roomController.Add(BossRoom);
        //    Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
        //    spawnInfo.type = Define.ObjectType.BossEnemy;
        //    spawnInfo.spawnPoint = new Vector3(8f, 75f, 0f);
        //    BossRoom.AddSpawnInfo(spawnInfo);
        //}
    }

    void LoadUI()
    {
        GameObject uIRoot = GameObject.Find("UIRoot");
        if (uIRoot == null)
        {
            uIRoot = new GameObject("UIRoot");
        }

        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/GameEndingPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/BossInfoPanel", uIRoot.transform);
    }

    public override void Clear()
    {
        Debug.Log("MainScene Clear()");
    }
}
