using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScene : BaseScene
{
    protected override void Init()
    {
        Managers.Game.SetState(new CharacterSelectState());

        // Load UI
        LoadUI("Prefabs/UI/Scene/UI_CharacterSelect");
    }

    Dictionary<string, GameObject> _originals = new Dictionary<string, GameObject>();

    // 이렇게 쓰고 싶으면 로드한 프리팹들을 관리할 Resource 매니저부터 만들어야 할듯
    // Resource 매니저 없이 쓰려면 그냥 무조건 프리팹 로드해서 쓰도록 하면 될듯
    GameObject LoadUI(string path)
    {
        // UIRoot 찾기
        GameObject uiRoot = GameObject.Find("UIRoot");
        if (uiRoot == null)
            uiRoot = new GameObject("UIRoot");

        // original이 이미 로드되어 있는지 확인
        if (_originals.ContainsKey(path) == false)
        {
            GameObject original = Resources.Load<GameObject>(path);
            _originals.Add(path, original);
        }

        // 리소스 올리기
        GameObject instance = Instantiate(_originals[path], uiRoot.transform);

        return instance;
    }

    public override void Clear()
    {
        Debug.Log("TestStartScene Clear()");
    }
}
