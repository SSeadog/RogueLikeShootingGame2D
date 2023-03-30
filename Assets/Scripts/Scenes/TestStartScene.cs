using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScene : BaseScene
{
    protected override void Init()
    {
        Managers.Game.SetState(new CharacterSelectState());

        // Load UI
        Managers.Resource.Instantiate("Prefabs/UI/Scene/CharacterSelectUI");
    }

    public override void Clear()
    {
        Debug.Log("TestStartScene Clear()");
    }
}
