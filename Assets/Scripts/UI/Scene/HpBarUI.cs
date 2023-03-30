using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUI : UIBase
{
    private void Update()
    {
        Debug.Log($"Player Hp : {Managers.Game.player}");
    }
}
