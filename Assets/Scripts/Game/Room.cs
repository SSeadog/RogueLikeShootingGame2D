using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string name;
    public float posX = 10f;
    public float posY = 10f;
    public float sizeX = 10f;
    public float sizeY = 10f;

    public List<Data.TriggerInfo> triggerInfo = new List<Data.TriggerInfo>();
    public List<Data.SpawnInfo> spawnInfo = new List<Data.SpawnInfo>();
    public List<Data.DoorInfo> doorInfo = new List<Data.DoorInfo>();

    private bool _isDiscovered = false;
    private GameObject _roomInstance;
    private List<GameObject> _triggerInstances = new List<GameObject>();
    private List<GameObject> _doorInstances = new List<GameObject>();

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
            _triggerInstances.Add(instance);
        }
    }

    public void AddSpawnInfo(Data.SpawnInfo info)
    {
        spawnInfo.Add(info);
    }

    public void Generate()
    {
        if (_isDiscovered)
            return;

        _isDiscovered = true;
        Spawn();
        CloseDoors();
    }

    private void Spawn()
    {
        foreach (Data.DoorInfo info in doorInfo)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/" + info.type.ToString(), _roomInstance.transform);
            instance.transform.position = new Vector3(info.posX, info.posY, 0f);
            _doorInstances.Add(instance);
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
        foreach (GameObject door in _doorInstances)
        {
            door.SetActive(true);
        }
    }

    public void OpenDoors()
    {
        foreach (GameObject door in _doorInstances)
        {
            door.SetActive(false);
        }
    }
}
