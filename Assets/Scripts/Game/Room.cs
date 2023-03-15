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

        Debug.Log($"Room{roomId} �߰�!!");
        isDiscovered = true;
        Spawn();
    }

    private void Spawn()
    {
        foreach (Define.SpawnInfo info in _spawnInfos)
        {
            // Ÿ�Կ� �´� ��ҵ� �� �ν��Ͻ�ȭ�ϱ�
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Characters/" + info.type.ToString());
            instance.transform.position = info.spawnPoint;
        }
    }
}
