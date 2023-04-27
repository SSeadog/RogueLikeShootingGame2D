using TMPro;
using UnityEngine;

public class KeyUI : UIBase
{
    [SerializeField] private TMP_Text _text;

    void Update()
    {
        _text.text = Managers.Game.Key.ToString();
    }
}
