using UnityEngine;

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
