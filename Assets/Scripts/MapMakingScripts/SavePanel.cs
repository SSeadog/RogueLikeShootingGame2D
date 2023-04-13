using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public void OnLoadButtonClick()
    {
        // ������ �Ŵ����� �̹� json�����Ͱ� �ε�Ǿ� ���� ��
        // �װ� �״�� �̿��ϸ� �ɵ�?
        LoadRoom();
    }

    void LoadRoom()
    {
        foreach(Room room in Managers.Data.roomData.rooms)
        {
            // RoomMaking �ε��ϱ�
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
