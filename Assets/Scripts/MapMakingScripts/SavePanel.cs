using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SavePanel : MonoBehaviour
{
    public void OnLoadButtonClick()
    {
        LoadRoom();
    }

    void LoadRoom()
    {
        // RoomData 파일 정보를 바로 읽어오는 것이 아니라 DataManager에서 로드해둔 정보를 기반으로 불러옴
        foreach(Room room in Managers.Data.roomData.rooms)
        {
            // RoomMaking 로드하기
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/RoomMaking");
            instance.transform.Find("InputField").GetComponent<InputField>().text = room.name;
            instance.transform.position = new Vector3(room.posX, room.posY);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(room.sizeX, room.sizeY);
            instance.GetComponent<BoxCollider2D>().size = new Vector2(room.sizeX, room.sizeY);

            // Trigger
            foreach (Define.TriggerInfo info in room.triggerInfo)
            {
                GameObject triggerInstance = Managers.Resource.Instantiate("Prefabs/Objects/RoomTriggerMaking", instance.transform);
                triggerInstance.GetComponent<RoomTrigger>().SetRoomName(name);
                triggerInstance.transform.localScale = new Vector3(info.sizeX, info.sizeY, 0);
                triggerInstance.transform.position = new Vector3(info.posX, info.posY, 0);
            }

            // Door
            foreach (Define.DoorInfo info in room.doorInfo)
            {
                GameObject doorInstance = Managers.Resource.Instantiate("Prefabs/Objects/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                doorInstance.transform.position = new Vector3(info.posX, info.posY, 0);
            }

            // Monster + Object
            foreach (Define.SpawnInfo info in room.spawnInfo)
            {
                if (info.type > Define.ObjectType.Object)
                {
                    GameObject objectInstance = Managers.Resource.Instantiate("Prefabs/Objects/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                    objectInstance.transform.position = info.GetPosition();
                    MakingObject makingObject = objectInstance.GetComponent<MakingObject>();
                    if (makingObject != null)
                        makingObject.parentRoom = instance;
                }
                else if (info.type > Define.ObjectType.Monster)
                {
                    GameObject monsterInstance = Managers.Resource.Instantiate("Prefabs/Characters/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                    monsterInstance.transform.position = info.GetPosition();
                    MakingObject makingObject = monsterInstance.GetComponent<MakingObject>();
                    if (makingObject != null)
                        makingObject.parentRoom = instance;
                }
            }
        }
    }

    public void OnSaveButtonClick()
    {
        SaveRoom();
    }

    void SaveRoom()
    {
        SaveRoomData data = new SaveRoomData();
        data.Name = "RoomSaveTest";
        data.Rooms = new List<Room>();

        List<GameObject> rooms = new List<GameObject>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Making");
        foreach (GameObject obj in objs)
        {
            if (obj.name == "RoomMaking")
                rooms.Add(obj);
        }

        foreach (GameObject room in rooms)
        {
            Room roomData = new Room();
            roomData.name = room.GetComponentInChildren<InputField>().text;
            roomData.posX = room.transform.position.x;
            roomData.posY = room.transform.position.y;
            roomData.sizeX = room.GetComponent<RectTransform>().sizeDelta.x;
            roomData.sizeY = room.GetComponent<RectTransform>().sizeDelta.y;
            data.Rooms.Add(roomData);

            // 트리거 저장
            for (int i = 0; i < room.transform.childCount; i++)
            {
                if (room.transform.GetChild(i).name == "RoomTriggerMaking")
                {
                    Transform trigger = room.transform.GetChild(i);

                    Define.TriggerInfo triggerData = new Define.TriggerInfo();
                    triggerData.posX = trigger.position.x;
                    triggerData.posY = trigger.position.y;
                    triggerData.sizeX = trigger.GetComponent<RectTransform>().sizeDelta.x / 100;
                    triggerData.sizeY = trigger.GetComponent<RectTransform>().sizeDelta.y / 100;
                    roomData.triggerInfo.Add(triggerData);
                }
            }

            // 문 저장
            for (int i = 0; i < room.transform.childCount; i++)
            {
                if (room.transform.GetChild(i).name == "DoorHorizontalMaking" || room.transform.GetChild(i).name == "DoorVerticalMaking")
                {
                    Transform door = room.transform.GetChild(i);

                    Define.DoorInfo doorData = new Define.DoorInfo();
                    Define.ObjectType makingType = Enum.Parse<Define.ObjectType>(room.transform.GetChild(i).name);
                    doorData.type = Util.ConvertMakingTypeToObjectType(makingType);
                    doorData.posX = door.position.x;
                    doorData.posY = door.position.y;
                    roomData.doorInfo.Add(doorData);
                }
            }

            MakingObject[] makingObjects = GameObject.FindObjectsOfType<MakingObject>();
            // 오브젝트 저장
            for (int i = 0; i < makingObjects.Length; i++)
            {
                if (makingObjects[i].parentRoom.GetComponentInChildren<InputField>().text != roomData.name)
                    continue;

                if (makingObjects[i].type > Define.ObjectType.Object)
                {
                    Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
                    spawnInfo.type = Util.ConvertMakingTypeToObjectType(makingObjects[i].type);
                    spawnInfo.posX = makingObjects[i].transform.position.x;
                    spawnInfo.posY = makingObjects[i].transform.position.y;
                    roomData.spawnInfo.Add(spawnInfo);
                }
            }

            // 몬스터 저장. 매 room마다 모든 MakingObject를 대상을 찾기에 비효율적일 수도 있음
            for (int i = 0; i < makingObjects.Length; i++)
            {
                if (makingObjects[i].parentRoom.GetComponentInChildren<InputField>().text != roomData.name)
                    continue;

                if (makingObjects[i].type > Define.ObjectType.Monster && makingObjects[i].type < Define.ObjectType.Object)
                {
                    Define.SpawnInfo spawnInfo = new Define.SpawnInfo();
                    spawnInfo.type = Util.ConvertMakingTypeToObjectType(makingObjects[i].type);
                    spawnInfo.posX = makingObjects[i].transform.position.x;
                    spawnInfo.posY = makingObjects[i].transform.position.y;
                    roomData.spawnInfo.Add(spawnInfo);
                }
            }
        }

        string jsonData = Util.ToJson<SaveRoomData>(data);
        Debug.Log(jsonData);
        Debug.Log(Application.persistentDataPath);
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/TestRoomData.json"))
        {
            sw.Write(jsonData);
        }
    }
}

[System.Serializable]
public class SaveRoomData
{
    public string Name;
    public string Map;
    public List<Room> Rooms;
}