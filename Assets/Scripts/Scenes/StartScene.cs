using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Game.SetState(new CharacterSelectState());

        // Load UI
        Managers.Resource.Instantiate("Prefabs/UI/Scene/CharacterSelectUI", Managers.Ui.UiRoot.transform);
    }

    public override void Clear()
    {
    }
}
