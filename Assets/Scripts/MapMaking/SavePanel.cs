using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private MapSelectPopup mapSelectPopup;

    public void OnLoadButtonClick()
    {
        mapSelectPopup.ShowPopup();
    }

    public void OnSaveButtonClick()
    {
        SaveRoom();
    }

    public void SetStageName(string name) {
        nameText.text = name;
    }

    void SaveRoom()
    {
        SaveRoomData data = new SaveRoomData();
        data.Name = "RoomSave";
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

            // Ʈ���� ����
            for (int i = 0; i < room.transform.childCount; i++)
            {
                if (room.transform.GetChild(i).name == "RoomTriggerMaking")
                {
                    Transform trigger = room.transform.GetChild(i);

                    Data.TriggerInfo triggerData = new Data.TriggerInfo();
                    triggerData.posX = trigger.position.x;
                    triggerData.posY = trigger.position.y;
                    triggerData.sizeX = trigger.GetComponent<RectTransform>().sizeDelta.x / 100;
                    triggerData.sizeY = trigger.GetComponent<RectTransform>().sizeDelta.y / 100;
                    roomData.triggerInfo.Add(triggerData);
                }
            }

            // �� ����
            for (int i = 0; i < room.transform.childCount; i++)
            {
                if (room.transform.GetChild(i).name == "DoorHorizontalMaking" || room.transform.GetChild(i).name == "DoorVerticalMaking")
                {
                    Transform door = room.transform.GetChild(i);

                    Data.DoorInfo doorData = new Data.DoorInfo();
                    Define.ObjectType makingType = Enum.Parse<Define.ObjectType>(room.transform.GetChild(i).name);
                    doorData.type = Util.ConvertMakingTypeToObjectType(makingType);
                    doorData.posX = door.position.x;
                    doorData.posY = door.position.y;
                    roomData.doorInfo.Add(doorData);
                }
            }

            MakingObject[] makingObjects = GameObject.FindObjectsOfType<MakingObject>();
            // ������Ʈ ����
            for (int i = 0; i < makingObjects.Length; i++)
            {
                if (makingObjects[i].parentRoom.GetComponentInChildren<InputField>().text != roomData.name)
                    continue;

                if (makingObjects[i].type > Define.ObjectType.Object)
                {
                    Data.SpawnInfo spawnInfo = new Data.SpawnInfo();
                    spawnInfo.type = Util.ConvertMakingTypeToObjectType(makingObjects[i].type);
                    spawnInfo.posX = makingObjects[i].transform.position.x;
                    spawnInfo.posY = makingObjects[i].transform.position.y;
                    roomData.spawnInfo.Add(spawnInfo);
                }
            }

            // ���� ����. �� room���� ��� MakingObject�� ����� ã�⿡ ��ȿ������ ���� ����
            for (int i = 0; i < makingObjects.Length; i++)
            {
                if (makingObjects[i].parentRoom.GetComponentInChildren<InputField>().text != roomData.name)
                    continue;

                if (makingObjects[i].type > Define.ObjectType.Monster && makingObjects[i].type < Define.ObjectType.Object)
                {
                    Data.SpawnInfo spawnInfo = new Data.SpawnInfo();
                    spawnInfo.type = Util.ConvertMakingTypeToObjectType(makingObjects[i].type);
                    spawnInfo.posX = makingObjects[i].transform.position.x;
                    spawnInfo.posY = makingObjects[i].transform.position.y;
                    roomData.spawnInfo.Add(spawnInfo);
                }
            }
        }

        string jsonData = Util.ToJson<SaveRoomData>(data);
        Debug.Log(Application.persistentDataPath);
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/RoomData.json"))
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