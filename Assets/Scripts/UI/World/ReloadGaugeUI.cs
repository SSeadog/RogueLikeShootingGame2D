using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGaugeUI : MonoBehaviour
{
    [SerializeField] GameObject _parentUI;
    [SerializeField] GameObject _bar;

    float _width;

    void Start()
    {
        _width = _parentUI.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void FillGauge(float endTime = 1f)
    {
        StartCoroutine(CoFillGauge(endTime));
    }

    IEnumerator CoFillGauge(float endTime = 1f)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        float time = 0f;
        while (time < endTime)
        {
            Debug.Log((time / endTime) * _width);
            _bar.GetComponent<RectTransform>().anchoredPosition = new Vector2((time / endTime) * _width, 0f);
            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
