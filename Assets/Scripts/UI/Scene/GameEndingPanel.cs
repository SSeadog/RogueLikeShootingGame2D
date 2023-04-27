using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndingPanel : UIBase
{
    protected override void Init()
    {
        base.Init();
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void OnToLobbyButtonClicked()
    {
        Managers.Scene.LoadScene("StartScene");
    }
}
