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
    GameObject _curSelectInstance;
    public InputController _inputController;
    string _instanceToolMakingPath = "Prefabs/Objects/InstanceToolMaking";

    public GameObject CurSelectInstance
    {
        get { return _curSelectInstance; }
        set
        {
            if (_curSelectInstance != null)
            {
                GameObject tool = _curSelectInstance.transform.Find("InstanceToolMaking").gameObject;
                if (tool != null)
                    Destroy(tool);
            }

            _curSelectInstance = value;

            if (_curSelectInstance != null )
            {
                if (_curSelectInstance.GetComponent<RectTransform>() != null)
                {
                    GameObject instance = Managers.Resource.Instantiate(_instanceToolMakingPath, _curSelectInstance.transform);
                    instance.GetComponent<InstanceToolMaking>().Init(_curSelectInstance.GetComponent<BoxCollider2D>());
                }
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        _inputController = GameObject.FindObjectOfType<InputController>();
        Managers.Game.SetState(new MapSelectState());
    }

    public string GetCurSelectObjectPath()
    {
        string path = null;
        if (_curSelectObjectType > Define.ObjectType.Object)
        {
            path = "Prefabs/Objects/" + _curSelectObjectType.ToString();
        }
        else if (_curSelectObjectType > Define.ObjectType.Monster)
        {
            path = "Prefabs/Characters/" + _curSelectObjectType.ToString();
        }

        return path;
    }

    public override void Clear()
    {
    }
}
