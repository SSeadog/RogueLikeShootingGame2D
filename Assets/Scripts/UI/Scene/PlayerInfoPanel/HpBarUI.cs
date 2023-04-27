using UnityEngine.UI;

public class HpBarUI : UIBase
{
    private Image[] _icons = null;

    protected override void Init()
    {
        _icons = gameObject.GetComponentsInChildren<Image>();
    }

    public void SetHpBar(int hp)
    {
        if (_icons == null)
            Init();

        PaintHpBar(hp);
    }

    void PaintHpBar(int hp)
    {
        for (int i = 0; i < hp; i++)
        {
            _icons[i].enabled = true;
        }
        for (int i = hp; i < 10; i++)
        {
            _icons[i].enabled = false;
        }
    }
}
