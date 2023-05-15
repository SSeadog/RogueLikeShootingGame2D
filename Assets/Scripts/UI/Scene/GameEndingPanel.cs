using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameEndingPanel : UIBase
{
    // Todo
    // 다른 UI는 끄기
    private float _fadeTimer = 0f;
    private float _baseAlpha = 0.5f;
    private float _plusDelta = 0.01f;

    enum TMP_Texts
    {
        ResultTitleText,
        PlayerNameText,
        PlayTimeText,
        CollectedGoldText,
        KillCountText,
        DeathCauseText
    }

    enum GameObjects
    {
        DeathCauseRow
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(TMP_Texts));
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>(GameObjects.DeathCauseRow.ToString()).SetActive(false);
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
            // 승리 시 데이터
            Get<TMP_Text>(TMP_Texts.ResultTitleText.ToString()).text = "플레이어 승리!";
        }
        else
        {
            // 패배 시 데이터
            Get<TMP_Text>(TMP_Texts.ResultTitleText.ToString()).text = "플레이어 패배!";
            Get<GameObject>(GameObjects.DeathCauseRow.ToString()).SetActive(true);
            Get<TMP_Text>(TMP_Texts.DeathCauseText.ToString()).text = "사망 원인";
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
