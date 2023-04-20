using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanel : UIBase
{
    InputController _inputController;
    Button _curSelectButton;
    [SerializeField] SpawnPanel _spawnPanel; // 나중에 LoadUI를 통해서 UI매니저에 등록해서 호출하도록 변경 필요

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
        // 수정 필요
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
        // 현재 버튼 색깔 바꿔서 선택됐다는 표시해두기
        // 마우스에 반투명하게 호버시키기
        // 룸오브젝트는 오리지널에서 렌더러 붙여넣기
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
