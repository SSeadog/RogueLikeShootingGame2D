using System.Collections;
using UnityEngine;

public class ReloadGaugeUI : UIBase
{
    enum GameObjects
    {
        Background,
        Foreground
    }

    enum RectTransforms
    {
        Bar
    }

    private float _width;

    protected override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<RectTransform>(typeof(RectTransforms));
        _width = gameObject.GetComponent<RectTransform>().sizeDelta.x;

        Get<GameObject>(GameObjects.Background.ToString()).SetActive(false);
        Get<GameObject>(GameObjects.Foreground.ToString()).SetActive(false);
    }

    public void FillGauge(float endTime = 1f)
    {
        StartCoroutine(CoFillGauge(endTime));
    }

    public void Stop()
    {
        StopAllCoroutines();
        Get<RectTransform>(RectTransforms.Bar.ToString()).anchoredPosition = new Vector2(0f, 0f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator CoFillGauge(float endTime = 1f)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        RectTransform bar = Get<RectTransform>(RectTransforms.Bar.ToString());
        float time = 0f;
        while (time < endTime)
        {
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2((time / endTime) * _width, 0f);
            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
