using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyPanel : UIBase
{
    [SerializeField] Text _selectObjectNameText;

    protected override void Init()
    {
        base.Init();
    }

    public void SetText(string text)
    {
        _selectObjectNameText.text = text;
    }

    public void OnDeleteButtonClick()
    {
        GameObject go = Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance;
        Destroy(go);
        Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance = null;
    }

}
