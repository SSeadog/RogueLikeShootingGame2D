using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemBase
{
    int value = 1;

    public override void Effect()
    {
        Managers.Game.gold += value;
        Destroy(gameObject);

        Debug.Log(Managers.Game.gold);
    }
}
