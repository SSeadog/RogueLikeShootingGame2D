using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : UIBase
{
    private enum Texts
    {
        Name
    }

    private enum Buttons
    {
        MapItem
    }

    private int stageId;

    public void SetData(int stageId)
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        this.stageId = stageId;

        Get<TMP_Text>(Texts.Name.ToString()).text = "Stage" + stageId;
        Get<Button>(Buttons.MapItem.ToString()).onClick.AddListener(OnButtonClicked);
    }

    protected override void InitImediately()
    {
        base.InitImediately();

        // Bind<TMP_Text>(typeof(Texts));
        // Bind<Button>(typeof(Buttons));
    }

    private void OnButtonClicked() {
        Debug.Log("Stage" + stageId + " 클릭!!!");

        Managers.Game.StageId = stageId;
        // 선택한 씬의 맵 불러오기
        LoadRoom();

        FindObjectOfType<SavePanel>().SetStageName("Stage " + stageId);
        GetComponentInParent<MapSelectPopup>().HidePopup();
    }

    private void LoadRoom()
    {
        // RoomData ���� ������ �ٷ� �о���� ���� �ƴ϶� DataManager���� �ε��ص� ������ ������� �ҷ���
        foreach(Room room in Managers.Data.RoomData.rooms)
        {
            // RoomMaking �ε��ϱ�
            GameObject instance = Managers.Resource.Instantiate("Prefabs/MapMaking/Objects/RoomMaking");
            instance.transform.Find("InputField").GetComponent<InputField>().text = room.name;
            instance.transform.position = new Vector3(room.posX, room.posY);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(room.sizeX, room.sizeY);
            instance.GetComponent<BoxCollider2D>().size = new Vector2(room.sizeX, room.sizeY);

            // Trigger
            foreach (Data.TriggerInfo info in room.triggerInfo)
            {
                GameObject triggerInstance = Managers.Resource.Instantiate("Prefabs/MapMaking/Objects/RoomTriggerMaking", instance.transform);
                triggerInstance.GetComponent<RoomTrigger>().SetRoomName(name);
                triggerInstance.transform.localScale = new Vector3(info.sizeX, info.sizeY, 0);
                triggerInstance.transform.position = new Vector3(info.posX, info.posY, -1f);
            }

            // Door
            foreach (Data.DoorInfo info in room.doorInfo)
            {
                GameObject doorInstance = Managers.Resource.Instantiate("Prefabs/MapMaking/Objects/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                doorInstance.transform.position = new Vector3(info.posX, info.posY, -1f);
            }

            // Monster + Object
            foreach (Data.SpawnInfo info in room.spawnInfo)
            {
                if (info.type > Define.ObjectType.Object)
                {
                    GameObject objectInstance = Managers.Resource.Instantiate("Prefabs/MapMaking/Objects/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                    objectInstance.transform.position = new Vector3(info.posX, info.posY, -1f);
                    MakingObject makingObject = objectInstance.GetComponent<MakingObject>();
                    if (makingObject != null)
                        makingObject.parentRoom = instance;
                }
                else if (info.type > Define.ObjectType.Monster)
                {
                    GameObject monsterInstance = Managers.Resource.Instantiate("Prefabs/Characters/" + Util.ConvertObjectTypeToMakingType(info.type), instance.transform);
                    monsterInstance.transform.position = new Vector3(info.posX, info.posY, -1f);
                    MakingObject makingObject = monsterInstance.GetComponent<MakingObject>();
                    if (makingObject != null)
                        makingObject.parentRoom = instance;
                }
            }
        }
    }

}
