using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndingPanel : UIBase
{
    // Todo
    // 실제 데이터 넣기
    // 다른 UI는 끄기

    [SerializeField] private TMP_Text _resultTitleText;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playTimeText;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _killCountText;
    [SerializeField] private GameObject _deathCauseRow;
    [SerializeField] private TMP_Text _deathCauseText;

    private float _fadeTimer = 0f;
    private float _baseAlpha = 0.5f;
    private float _plusDelta = 0.01f;

    protected override void Init()
    {
        base.Init();
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
            _resultTitleText.text = "플레이어 승리!";
        }
        else
        {
            // 패배 시 데이터
            _resultTitleText.text = "플레이어 패배!";
            _deathCauseRow.SetActive(true);
            _deathCauseText.text = "사망 원인";
        }

        _playerNameText.text = "캐릭터 이름";
        _playTimeText.text = "00:00";
        _goldText.text = "111";
        _killCountText.text = "999";
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
