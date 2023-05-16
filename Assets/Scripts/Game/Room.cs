using System.Collections.Generic;
using UnityEngine;

public class Room
{

    public string name;
    public float posX = 10f;
    public float posY = 10f;
    public float sizeX = 10f;
    public float sizeY = 10f;

    public bool isDiscovered = false;

    public List<Data.TriggerInfo> triggerInfo = new List<Data.TriggerInfo>();
    public List<Data.SpawnInfo> spawnInfo = new List<Data.SpawnInfo>();
    public List<Data.DoorInfo> doorInfo = new List<Data.DoorInfo>();

    public List<GameObject> triggerInstances = new List<GameObject>();
    public List<GameObject> doorInstances = new List<GameObject>();

    private GameObject _roomInstance;

    public void Init()
    {
        GameObject go = new GameObject();
        go.name = name;
        _roomInstance = go;
        
        foreach (Data.TriggerInfo info in triggerInfo)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/RoomTrigger", _roomInstance.transform);
            instance.GetComponent<RoomTrigger>().SetRoomName(name);
            instance.transform.localScale = new Vector3(info.sizeX, info.sizeY, 0);
            instance.transform.position = new Vector3(info.posX, info.posY, 0);
            triggerInstances.Add(instance);
        }
    }

    public void AddSpawnInfo(Data.SpawnInfo info)
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

        isDiscovered = true;
        Spawn();
        CloseDoors();
    }

    private void Spawn()
    {
        foreach (Data.DoorInfo info in doorInfo)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/" + info.type.ToString(), _roomInstance.transform);
            instance.transform.position = new Vector3(info.posX, info.posY, 0f);
            doorInstances.Add(instance);
        }

        foreach (Data.SpawnInfo info in spawnInfo)
        {
            if (info.type > Define.ObjectType.Object)
            {
                GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/" + info.type.ToString(), _roomInstance.transform);
                instance.transform.position = new Vector3(info.posX, info.posY, 0f);
            }
            else if (info.type > Define.ObjectType.Monster)
            {
                GameObject instance = Managers.Resource.Instantiate("Prefabs/Characters/" + info.type.ToString(), _roomInstance.transform);
                instance.transform.position = new Vector3(info.posX, info.posY, 0f);
                Managers.Game.AddSpawnedEnemy(instance.GetComponent<EnemyControllerBase>());
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
