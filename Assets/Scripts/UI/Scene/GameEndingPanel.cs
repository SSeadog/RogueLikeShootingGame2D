using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEndingPanel : UIBase
{
    private float _fadeTime = 1f;
    private float _fadeTimer = 0f;

    private float _baseAlpha = 127f;

    protected override void Init()
    {
        base.Init();
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(CoShow());
    }

    IEnumerator CoShow()
    {
        Image panel = gameObject.GetComponent<Image>();

        while (_fadeTimer < _fadeTime)
        {
            _fadeTimer += 0.05f;
            Color color = panel.color;
            color.a = (_fadeTimer / _fadeTime) * _baseAlpha;
            panel.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnToLobbyButtonClicked()
    {
        Managers.Scene.LoadScene("StartScene");
    }
}
