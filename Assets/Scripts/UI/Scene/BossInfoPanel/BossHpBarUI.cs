using UnityEngine;

public class BossHpBarUI : UIBase
{
    [SerializeField] Transform _hpBar;

    public void SetHpBar(float percent)
    {
        _hpBar.localScale = new Vector3(percent, 1f, 1f);
    }
}
