using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectItemUI : UIBase
{
    private int stageId;

    enum Texts
    {
        Name
    }
    enum Images
    {
        Thumb
    }

    enum Buttons
    {
        StageSelectItemUI
    }

    protected override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        Get<TMP_Text>(Texts.Name.ToString()).text = "Stage" + stageId;
        Get<Button>(Buttons.StageSelectItemUI.ToString()).onClick.AddListener(OnButtonClicked);
    }

    public void SetData(int stageId)
    {
        this.stageId = stageId;
    }

    private void OnButtonClicked() {
        Debug.Log("Stage" + stageId + " 클릭!!!");

        Managers.Game.StageId = stageId;
        Managers.Scene.LoadScene("MainScene");
    }
}
