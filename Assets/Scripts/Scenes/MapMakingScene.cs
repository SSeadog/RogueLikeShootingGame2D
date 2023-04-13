using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  EditMode�� FSM���� �����
 *  ���� ���� ���� �ٸ� Input �Լ��� �����ϱ�
 *  ���� ����� ������ �Լ��� ���� ���
 *  �Լ� ����� ���� ���콺 ��ǲ���� Delegate�� ��ϼ����صα�
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
                Destroy(tool);
            }

            _curSelectInstance = value;

            if (_curSelectInstance != null )
            {
                GameObject instance = Managers.Resource.Instantiate(_instanceToolMakingPath, _curSelectInstance.transform);
                instance.GetComponent<InstanceToolMaking>().Init(_curSelectInstance.GetComponent<BoxCollider2D>());
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
        return "Prefabs/Objects/" + _curSelectObjectType.ToString();
    }

    public override void Clear()
    {
    }
}
