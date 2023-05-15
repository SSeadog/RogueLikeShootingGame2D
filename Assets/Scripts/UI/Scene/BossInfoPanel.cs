using TMPro;
using UnityEngine;

public class BossInfoPanel : UIBase
{
    enum TMP_Texts
    {
        BossNameUI
    }
    enum Transforms
    {
        BossHpBarForeground
    }

    protected override void Init()
    {
        base.Init();
        Bind<Transform>(typeof(Transforms));
        Bind<TMP_Text>(typeof(TMP_Texts));

        gameObject.SetActive(false);
    }

    public void SetBossName(string name)
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
        Get<TMP_Text>(TMP_Texts.BossNameUI.ToString()).text = name;
    }

    public void SetBossHpBar(float percent)
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        if (percent < 0f)
            percent = 0f;

        Get<Transform>(Transforms.BossHpBarForeground.ToString()).localScale = new Vector3(percent, 1f, 1f);
    }
}
