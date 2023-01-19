using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterItem : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemName
    }

    string _name;

    protected override void Init()
    {
        base.Init();

        Bind(typeof(GameObjects));

        Transform nameTr = Get<Transform>(GameObjects.ItemName.ToString());
        nameTr.GetComponent<Text>().text = _name;
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
