using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndingPanel : UIBase
{
    // Todo
    // �ٸ� UI�� ����
    private float _fadeTimer = 0f;
    private float _baseAlpha = 0.5f;
    private float _plusDelta = 0.01f;

    enum TMP_Texts
    {
        ResultTitleText,
        PlayerNameText,
        PlayTimeText,
        CollectedGoldText,
        KillCountText
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(TMP_Texts));
        gameObject.SetActive(false);
    }

    public void Show(bool isWin, float fadeTime = 1f)
    {
        gameObject.SetActive(true);
        SetData(isWin);
        StartCoroutine(CoShow(fadeTime));
    }

    public void SetData(bool isWin)
    {
        if (isWin)
        {
            // �¸� �� ������
            Get<TMP_Text>(TMP_Texts.ResultTitleText.ToString()).text = "�÷��̾� �¸�!";
        }
        else
        {
            // �й� �� ������
            Get<TMP_Text>(TMP_Texts.ResultTitleText.ToString()).text = "�÷��̾� �й�!";
        }

        string playerName = ((Define.ObjectType)Managers.Game.PlayerId).ToString();
        Get<TMP_Text>(TMP_Texts.PlayerNameText.ToString()).text = Managers.Data.PlayerStatDict[playerName].name;
        Get<TMP_Text>(TMP_Texts.PlayTimeText.ToString()).text = Managers.Game.PlayTime.ToString();
        Get<TMP_Text>(TMP_Texts.CollectedGoldText.ToString()).text = Managers.Game.Gold.ToString();
        Get<TMP_Text>(TMP_Texts.KillCountText.ToString()).text = Managers.Game.KillCount.ToString();
    }

    IEnumerator CoShow(float fadeTime)
    {
        Image panel = gameObject.GetComponent<Image>();

        while (_fadeTimer < fadeTime)
        {
            _fadeTimer += _plusDelta;
            Color color = panel.color;
            color.a = (_fadeTimer / fadeTime) * _baseAlpha;
            panel.color = color;
            yield return _plusDelta;
        }
    }

    public void OnToLobbyButtonClicked()
    {
        Managers.Scene.LoadScene("StartScene");
    }
}
