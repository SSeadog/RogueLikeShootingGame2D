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
