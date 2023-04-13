using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyPanel : UIBase
{
    Text _selectObjectNameText;

    protected override void Init()
    {
        base.Init();
        _selectObjectNameText = transform.Find("SelectObjectNameText").GetComponent<Text>();
    }

    public void SetText(string text)
    {
        _selectObjectNameText.text = text;
    }

    public void OnDeleteButtonClick()
    {
        GameObject go = (Managers.Scene.currentScene as MapMakingScene).CurSelectInstance;
        Destroy(go);
        (Managers.Scene.currentScene as MapMakingScene).CurSelectInstance = null;
    }

}
