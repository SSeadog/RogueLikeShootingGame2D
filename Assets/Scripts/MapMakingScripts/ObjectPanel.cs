using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanel : UIBase
{
    InputController _inputController;

    Button _curSelectButton;

    Button CurSelectButton
    {
        get
        {
            return _curSelectButton;
        }

        set
        {
            if (_curSelectButton != null)
                _curSelectButton.GetComponent<Image>().color = Color.white;

            if (_curSelectButton == value)
            {
                _curSelectButton = null;
            }
            else
            {
                _curSelectButton = value;
                _curSelectButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    [SerializeField] Button _instanceButton;
    [SerializeField] Button _roomButton;
    [SerializeField] Button _triggerButton;
    [SerializeField] Button _monsterButton;
    [SerializeField] Button _objectButton;

    void Start()
    {
        _inputController = GameObject.FindObjectOfType<InputController>();
    }

    public void OnInstanceButtonClick()
    {
        _inputController.SelectSpawnObject(Define.ObjectType.ObjectEnd);
        Managers.Game.SetState(new MapInstanceSelectState());
        CurSelectButton = _instanceButton;
    }

    public void OnRoomButtonClick()
    {
        // ���� ��ư ���� �ٲ㼭 ���õƴٴ� ǥ���صα�
        // ���콺�� �������ϰ� ȣ����Ű��
        // �������Ʈ�� �������ο��� ������ �ٿ��ֱ�
        _inputController.SelectSpawnObject(Define.ObjectType.RoomMaking);
        Managers.Game.SetState(new MapRoomSpawnState());
        CurSelectButton = _roomButton;
    }

    public void OnTriggerButtonClick()
    {
        _inputController.SelectSpawnObject(Define.ObjectType.RoomTriggerMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
        CurSelectButton = _triggerButton;
    }

    public void OnMonsterButtonClick()
    {

    }

    public void OnObjectButtonClick()
    {

    }

    public void OnSaveButtonClick()
    {
    }
}
