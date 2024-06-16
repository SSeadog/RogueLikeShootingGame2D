using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Game.SetState(new CharacterSelectState());

        // Load UI
        GameObject characterSelectUI = Managers.Game.LoadUI("Prefabs/UI/Scene/CharacterSelectUI", Managers.Ui.UiRoot.transform);
        GameObject stageSelectUI = Managers.Game.LoadUI("Prefabs/UI/Scene/StageSelectUI");
        stageSelectUI.SetActive(false);
    }

    public override void Clear()
    {
    }
}
