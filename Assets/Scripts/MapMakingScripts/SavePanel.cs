using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public void OnLoadButtonClick()
    {
        LoadRoom();
    }

    void LoadRoom()
    {
        foreach(Room room in Managers.Data.roomData.rooms)
        {
            // RoomMaking 로드하기
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Objects/RoomMaking");
            instance.transform.Find("InputField").GetComponent<InputField>().text = room.name;
            instance.transform.position = new Vector3(room.posX, room.posY);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(room.sizeX, room.sizeY);
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
        }

        Debug.Log(Util.ToJson<SaveRoomData>(data));
    }
}

[System.Serializable]
public class SaveRoomData
{
    public string Name;
    public string Map;
    public List<Room> Rooms;
}