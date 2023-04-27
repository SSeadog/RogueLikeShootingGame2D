using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        Managers.Game.SetState(new CharacterSelectState());

        // Load UI
        Managers.Resource.Instantiate("Prefabs/UI/Scene/CharacterSelectUI");
    }

    public override void Clear()
    {
    }
}
