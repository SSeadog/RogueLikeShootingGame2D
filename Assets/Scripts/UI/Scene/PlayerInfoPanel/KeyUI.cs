using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyUI : UIBase
{
    [SerializeField] TMP_Text _text;

    void Update()
    {
        _text.text = Managers.Game.key.ToString();
    }
}
