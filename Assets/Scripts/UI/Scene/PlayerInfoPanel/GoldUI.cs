using TMPro;
using UnityEngine;

public class GoldUI : UIBase
{
    [SerializeField] TMP_Text _text;

    void Update()
    {
        _text.text = Managers.Game.Gold.ToString();
    }
}
