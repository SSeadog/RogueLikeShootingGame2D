using UnityEngine;

public class MainScene : BaseScene
{
    private int _curFrame;
    private float _frameTimer;
    private float _frameRate = 0.1f;
    [SerializeField] private Texture2D[] _textureArray;

    protected override void Init()
    {
        base.Init();

        Managers.Game.SetState(new MainInitState());
        string playerName = ((Define.ObjectType)Managers.Game.PlayerId).ToString();
        GameObject player = Managers.Resource.Instantiate(Managers.Data.PlayerStatDict[playerName].objectPath);
        GameObject camera = Managers.Resource.Instantiate("Prefabs/Main Camera");
        camera.GetComponent<CameraController>().Init(player);

        SetInitItems();
        LoadMainSceneUI();

#if UNITY_EDITOR
        for (int i = 0; i < _textureArray.Length; i++)
        {
            _textureArray[i].alphaIsTransparency = true;
        }
#endif
    }

    private void Update()
    {
        _frameTimer -= Time.deltaTime;
        if (_frameTimer <= 0f)
        {
            _curFrame = (_curFrame + 1) % _textureArray.Length;
            Cursor.SetCursor(_textureArray[_curFrame], new Vector2(10, 10), CursorMode.Auto);
            _frameTimer += _frameRate;
        }
    }

    void SetInitItems()
    {
        Managers.Game.Gold = 0;
        Managers.Game.Key = 0;
        Managers.Game.Grenade = 2;
    }

    void LoadMainSceneUI()
    {
        GameObject uIRoot = Managers.Ui.UiRoot;

        Managers.Game.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel", uIRoot.transform);
        Managers.Game.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel", uIRoot.transform);
        Managers.Game.LoadUI("Prefabs/UI/Scene/GameEndingPanel", uIRoot.transform);
        Managers.Game.LoadUI("Prefabs/UI/Scene/BossInfoPanel", uIRoot.transform);
    }

    public override void Clear()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Managers.Pool.Clear();
    }
}
