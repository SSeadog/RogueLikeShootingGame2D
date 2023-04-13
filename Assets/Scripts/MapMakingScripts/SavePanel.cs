using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public void OnLoadButtonClick()
    {
        // 데이터 매니저에 이미 json데이터가 로드되어 있을 것
        // 그거 그대로 이용하면 될듯?
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

    }
}
