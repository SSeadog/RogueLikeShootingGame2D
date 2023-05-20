using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<string, GameObject> _originals = new Dictionary<string, GameObject>();

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

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if (_originals.ContainsKey(path) == false)
        {
            T original = Resources.Load<T>(path);
            if (original == null)
            {
                Debug.Log($"리소스 로드 실패! {path}");
                return null;
            }

            return original;
        }

        return null;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        Load(path);
        if (_originals.ContainsKey(path) == false)
            return null;

        GameObject instance = GameObject.Instantiate(_originals[path], parent);
        instance.name = _originals[path].name;

        return instance;
    }

    public void Clear()
    {

    }
}