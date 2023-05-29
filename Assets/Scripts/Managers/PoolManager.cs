using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // poolObject 초기화를 어떻게 처리하지?

    private Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string, Stack<GameObject>>();

    public GameObject GetPoolObject(string poolObjectPrefabPath)
    {
        if (_pools.ContainsKey(poolObjectPrefabPath) == false)
        {
            _pools.Add(poolObjectPrefabPath, new Stack<GameObject>());
        }

        if (_pools[poolObjectPrefabPath].Count == 0)
        {
            GameObject newPoolObject = Managers.Resource.Instantiate(poolObjectPrefabPath);
            return newPoolObject;
        }

        return _pools[poolObjectPrefabPath].Pop();
    }

    public void StorePoolObject(string poolObjectPrefabPath, GameObject poolObject)
    {
        if (_pools.ContainsKey(poolObjectPrefabPath) == false)
        {
            _pools.Add(poolObjectPrefabPath, new Stack<GameObject>());
        }

        _pools[poolObjectPrefabPath].Push(poolObject);
        Debug.Log(_pools[poolObjectPrefabPath].Count);
    }
}
