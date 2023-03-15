using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<string, GameObject> _originals = new Dictionary<string, GameObject>();

    public void Init()
    {

    }

    public void Load(string path)
    {
        if (_originals.ContainsKey(path) == false)
        {
            GameObject original = Resources.Load<GameObject>(path);
            if (original == null)
            {
                Debug.Log($"리소스 로드 실패! {path}");
                return;
            }

            _originals[path] = original;
        }
    }

    public GameObject Instantiate(string path)
    {
        Load(path);
        if (_originals.ContainsKey(path) == false)
            return null;

        return GameObject.Instantiate(_originals[path]);
    }

    public void Clear()
    {

    }
}