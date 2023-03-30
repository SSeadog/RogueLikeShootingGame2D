using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemBase
{
    public override void Effect()
    {
        Managers.Game.key++;
    }
}
