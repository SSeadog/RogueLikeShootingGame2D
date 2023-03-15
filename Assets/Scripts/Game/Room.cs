using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int roomId;
    bool isDiscovered = false;
    List<Define.SpawnInfo> _spawnInfos = new List<Define.SpawnInfo>();

    public Room()
    {
    }

    public Room(int id)
    {
        roomId = id;
    }

    public void AddSpawnInfo(Define.SpawnInfo info)
    {
        _spawnInfos.Add(info);
    }

    public void Found()
    {
        if (isDiscovered)
            return;

        Debug.Log($"Room{roomId} 발견!!");
        isDiscovered = true;
        Spawn();
    }

    private void Spawn()
    {
        foreach (Define.SpawnInfo info in _spawnInfos)
        {
            // 타입에 맞는 요소들 쭉 인스턴스화하기
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Characters/" + info.type.ToString());
            instance.transform.position = info.spawnPoint;
        }
    }
}
