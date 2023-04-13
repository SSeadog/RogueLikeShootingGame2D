using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  EditMode를 FSM으로 만들고
 *  상태 따라 각각 다른 Input 함수들 연결하기
 *  상태 변경될 때마다 함수를 따로 등록
 *  함수 등록은 각각 마우스 인풋마다 Delegate로 등록설정해두기
 */

public class MapMakingScene : BaseScene
{
    public enum EditMode
    {
        None = 0,
        Spawn,
        Edit
    }

    public EditMode _editMode = EditMode.None;
    public Define.ObjectType _curSelectObjectType;
    public GameObject _curSelectInstance;
    public InputController _inputController;

    protected override void Init()
    {
        base.Init();
        _inputController = GameObject.FindObjectOfType<InputController>();
        Managers.Game.SetState(new MapSelectState());
    }

    public string GetCurSelectObjectPath()
    {
        return "Prefabs/Objects/" + _curSelectObjectType.ToString();
    }

    public override void Clear()
    {
    }
}
