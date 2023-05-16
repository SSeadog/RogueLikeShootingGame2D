public class Key : ItemBase
{
    public override void Effect(Stat stat)
    {
        Managers.Game.Key++;
    }
}
