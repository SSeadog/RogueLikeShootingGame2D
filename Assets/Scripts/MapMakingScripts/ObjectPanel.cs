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
        // 현재 버튼 색깔 바꿔서 선택됐다는 표시해두기
        // 마우스에 반투명하게 호버시키기
        // 룸오브젝트는 오리지널에서 렌더러 붙여넣기
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
