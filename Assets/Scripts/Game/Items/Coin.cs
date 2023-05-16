using UnityEngine;

public class Coin : ItemBase
{
    private int value = 1;

    public override void Effect(Stat stat)
    {
        Managers.Game.Gold += value;
        Destroy(gameObject);
    }
}
