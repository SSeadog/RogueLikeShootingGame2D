using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        // TestCode
        if (Managers.Game.PlayerId == 0)
            Managers.Game.PlayerId = 1;

        Managers.Game.SetState(new MainInitState());

        Managers.Game.RoomManager.FindRoom("StartRoom").Found();

        GameObject playerOriginal = Resources.Load<GameObject>("Prefabs/Characters/TestPlayer");
        GameObject cameraOriginal = Resources.Load<GameObject>("Prefabs/Main Camera");

        GameObject player = Instantiate(playerOriginal);
        player.name = playerOriginal.name;

        GameObject camera = Instantiate(cameraOriginal);
        camera.name = cameraOriginal.name;
        camera.GetComponent<CameraController>().Init(player);

        Managers.Game.Gold = 0;
        Managers.Game.Key = 0;
        Managers.Game.Grenade = 2;

        // Test
        Managers.Game.Gold += 100;

        LoadMainSceneUI();
    }

    void LoadMainSceneUI()
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
    }
}
