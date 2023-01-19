using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScene : BaseScene
{
    protected override void Init()
    {
        // Load UI
        GameObject UI_CharacterSelectOriginal = Resources.Load<GameObject>("Prefabs/UI/Scene/UI_CharacterSelect");
        Instantiate(UI_CharacterSelectOriginal).GetComponent<UI_CharacterSelect>();
    }
}
