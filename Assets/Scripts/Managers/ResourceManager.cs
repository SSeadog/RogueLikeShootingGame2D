using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<string, GameObject> _originals = new Dictionary<string, GameObject>();

    public void Init()
    {

    }

    public GameObject Load(string path)
    {
        if (_originals.ContainsKey(path) == false)
        {
            GameObject original = Resources.Load<GameObject>(path);
            if (original == null)
            {
                Debug.Log($"리소스 로드 실패! {path}");
                return null;
            }

            _originals[path] = original;
        }

        return _originals[path];
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        Load(path);
        if (_originals.ContainsKey(path) == false)
            return null;

        return GameObject.Instantiate(_originals[path], parent);
    }

    public GameObject LoadUI(string path, Transform parent = null)
    {
        GameObject instance = Instantiate(path, parent);

        UIBase uIBase = instance.GetComponent<UIBase>();
        if (uIBase != null)
        {
            Managers.Ui.AddUI(uIBase);
        }
        else
        {
            Debug.Log($"{path}의 uI에 UIBase가 없습니다!");
        }

        return instance;
    }

    public void Clear()
    {

    }
}