using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanel : UIBase
{
    InputController _inputController;
    Button _curSelectButton;
    [SerializeField] SpawnPanel _spawnPanel; // ���߿� LoadUI�� ���ؼ� UI�Ŵ����� ����ؼ� ȣ���ϵ��� ���� �ʿ�

    Button CurSelectButton
    {
        get
        {
            return _curSelectButton;
        }

        set
        {
            _spawnPanel.Hide();
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
        // ���� �ʿ�
        _inputController = GameObject.FindObjectOfType<InputController>();
    }

    public void OnInstanceButtonClick()
    {
        CurSelectButton = _instanceButton;
        _inputController.SelectSpawnObject(Define.ObjectType.ObjectEnd);
        Managers.Game.SetState(new MapInstanceSelectState());
    }

    public void OnRoomButtonClick()
    {
        // ���� ��ư ���� �ٲ㼭 ���õƴٴ� ǥ���صα�
        // ���콺�� �������ϰ� ȣ����Ű��
        // �������Ʈ�� �������ο��� ������ �ٿ��ֱ�
        CurSelectButton = _roomButton;
        _inputController.SelectSpawnObject(Define.ObjectType.RoomMaking);
        Managers.Game.SetState(new MapRoomSpawnState());
    }

    public void OnTriggerButtonClick()
    {
        CurSelectButton = _triggerButton;
        _inputController.SelectSpawnObject(Define.ObjectType.RoomTriggerMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnMonsterButtonClick()
    {
        CurSelectButton = _monsterButton;
        _spawnPanel.ShowMonsterSpawnPanel();
    }

    public void OnObjectButtonClick()
    {
        CurSelectButton = _objectButton;
        _spawnPanel.ShowObjectSpawnPanel();
    }

    public void OnSaveButtonClick()
    {
    }
}
