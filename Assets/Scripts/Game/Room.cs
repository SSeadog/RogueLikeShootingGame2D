using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room
{
    private GameObject _parent;

    public string name;
    public float posX = 10f;
    public float posY = 10f;
    public float sizeX = 10f;
    public float sizeY = 10f;

    public bool isDiscovered = false;

    public List<Define.TriggerInfo> triggerInfo = new List<Define.TriggerInfo>();
    public List<Define.SpawnInfo> spawnInfo = new List<Define.SpawnInfo>();
    public List<Define.DoorInfo> doorInfo = new List<Define.DoorInfo>();

    public List<GameObject> triggerInstances = new List<GameObject>();
    public List<GameObject> doorInstances = new List<GameObject>();


    public void Init()
    {
        GameObject go = new GameObject();
        go.name = name;
        _parent = go;
        
        foreach (Define.TriggerInfo info in triggerInfo)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/RoomTrigger", _parent.transform);
            instance.GetComponent<RoomTrigger>().SetRoomName(name);
            instance.transform.localScale = new Vector3(info.sizeX, info.sizeY, 0);
            instance.transform.position = new Vector3(info.posX, info.posY, 0);
            triggerInstances.Add(instance);
        }
    }

    public void AddSpawnInfo(Define.SpawnInfo info)
    {
        spawnInfo.Add(info);
    }

    public void Found()
    {
        if (isDiscovered)
            return;

        RoomEnterState state = new RoomEnterState();
        state.SetRoom(this);
        Managers.Game.SetState(state);

        Debug.Log($"Room{name} 발견!!");
        isDiscovered = true;
        Spawn();
        CloseDoors();
    }

    private void Spawn()
    {
        foreach (Define.DoorInfo info in doorInfo)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/" + info.type.ToString(), _parent.transform);
            instance.transform.position = new Vector3(info.posX, info.posY, 0);
            doorInstances.Add(instance);
        }

        foreach (Define.SpawnInfo info in spawnInfo)
        {
            if (info.type > Define.ObjectType.Object)
            {
                GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/" + info.type.ToString(), _parent.transform);
                instance.transform.position = new Vector3(info.posX, info.posY, 0);
            }
            else if (info.type > Define.ObjectType.Monster)
            {
                GameObject instance = Managers.Resource.Instantiate("Prefabs/Characters/" + info.type.ToString(), _parent.transform);
                instance.transform.position = info.GetPosition();
            }
        }
    }

    public void CloseDoors()
    {
        foreach (GameObject door in doorInstances)
        {
            // 문 닫히는 애니메이션 추가할 거면 door에 함수 만들어서 처리하기
            door.SetActive(true);
        }
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doorInstances)
        {
            door.SetActive(false);
        }
    }
}
