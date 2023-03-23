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

    // �̷��� ���� ������ �ε��� �����յ��� ������ Resource �Ŵ������� ������ �ҵ�
    // Resource �Ŵ��� ���� ������ �׳� ������ ������ �ε��ؼ� ������ �ϸ� �ɵ�
    GameObject LoadUI(string path)
    {
        // UIRoot ã��
        GameObject uiRoot = GameObject.Find("UIRoot");
        if (uiRoot == null)
            uiRoot = new GameObject("UIRoot");

        // original�� �̹� �ε�Ǿ� �ִ��� Ȯ��
        if (_originals.ContainsKey(path) == false)
        {
            GameObject original = Resources.Load<GameObject>(path);
            _originals.Add(path, original);
        }

        // ���ҽ� �ø���
        GameObject instance = Instantiate(_originals[path], uiRoot.transform);

        return instance;
    }

    public override void Clear()
    {
        Debug.Log("TestStartScene Clear()");
    }
}
