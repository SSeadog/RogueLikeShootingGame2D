using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : UIBase
{
    [SerializeField] Text _text;

    public void SetText(string itemName, int gold)
    {
        _text.text = $"{itemName}: {gold}";
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
