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

        GameObject player = Managers.Resource.Instantiate("Prefabs/Characters/Player");
        GameObject camera = Managers.Resource.Instantiate("Prefabs/Main Camera");
        camera.GetComponent<CameraController>().Init(player);

        SetInitItems();
        LoadMainSceneUI();
    }

    void SetInitItems()
    {
        Managers.Game.Gold = 0;
        Managers.Game.Key = 0;
        Managers.Game.Grenade = 2;
    }

    void LoadMainSceneUI()
    {
        GameObject uIRoot = Managers.Ui.UiRoot;

        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/GameEndingPanel", uIRoot.transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/BossInfoPanel", uIRoot.transform);
    }

    public override void Clear()
    {
    }
}
