using TMPro;
using UnityEngine;

public class BossInfoPanel : UIBase
{
    private TMP_Text _bossNameText;
    private BossHpBarUI _bossHpBarUI;

    void Awake()
    {
        GameObject bossNameUI = Managers.Resource.LoadUI("Prefabs/UI/Scene/BossInfoPanel/BossNameUI", transform);
        GameObject bossHpBarUI = Managers.Resource.LoadUI("Prefabs/UI/Scene/BossInfoPanel/BossHpBarUI", transform);

        _bossNameText = bossNameUI.GetComponent<TMP_Text>();
        _bossHpBarUI = bossHpBarUI.GetComponent<BossHpBarUI>();

        gameObject.SetActive(false);
    }

    void SetActive()
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
    }

    public void SetBossName(string name)
    {
        SetActive();
        _bossNameText.text = name;
    }

    public void SetBossHpBar(float percent)
    {
        SetActive();
        _bossHpBarUI.SetHpBar(percent);
    }
}
