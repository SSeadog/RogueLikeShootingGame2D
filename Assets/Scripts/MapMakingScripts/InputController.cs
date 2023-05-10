using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Input관련 작업을 InputManager로 두고 할지는 고민해봐야할 듯?
// 생각보다 할 일이 조금 있을 거 같음
public class InputController : MonoBehaviour
{
    bool _pressed = false;
    float _pressTime;

    float _maxZoomLevel = 35f;
    float _minZoomLevel = 1f;
    float _zoomLevel; // 확대가 많이 될 수록 값이 작아짐. 확대가 적을 수록 값이 높아짐. 1~35
    float _scrollSpeed;
    float _moveSpeed;

    Vector3 _beforeMousePos;

    Action mouseLeftButtonClick;
    Action mouseWheelDown;
    Action mouseWheelUp;
    Action mouseWheelButtonClick;

    public float ZoomLevel { get { return _zoomLevel; } }

    void Start()
    {
        _zoomLevel = Camera.main.orthographicSize;
        _scrollSpeed = 1f;
        _moveSpeed = 1f;

        mouseWheelUp += EnLarge;
        mouseWheelDown += Reduce;
        mouseWheelButtonClick += Move;
    }

    public void SetRoomSpawnMouseEvent()
    {
        ClearEventsByState();
        mouseLeftButtonClick += SpawnRoom;
    }

    public void SetObjectSpawnMouseEvent()
    {
        ClearEventsByState();
        mouseLeftButtonClick += SpawnObject;
    }

    public void SetInstanceSelectMouseEvenet()
    {
        ClearEventsByState();
        mouseLeftButtonClick += SelectInstance;
    }

    public void SetInstanceEditMouseEvent()
    {
        ClearEventsByState();
        mouseLeftButtonClick += SelectInstance;
    }

    void ClearEventsByState()
    {
        mouseLeftButtonClick = null;
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0f)
        {
            mouseWheelUp();
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            mouseWheelDown();
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            _pressed = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (_pressed)
                _pressTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_pressed && _pressTime < 0.2f)
            {
                mouseLeftButtonClick?.Invoke();
            }

            _pressed = false;
            _pressTime = 0f;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _beforeMousePos = _beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            mouseWheelButtonClick();
        }

        if (Input.GetMouseButtonUp(2))
        {
            _beforeMousePos = Vector3.zero;
        }
    }
    private void EnLarge()
    {
        _zoomLevel -= _scrollSpeed;
        if (_zoomLevel < _minZoomLevel)
            _zoomLevel = _minZoomLevel;
        Camera.main.orthographicSize = _zoomLevel;
    }

    private void Reduce()
    {
        _zoomLevel += _scrollSpeed;
        if (_zoomLevel > _maxZoomLevel)
            _zoomLevel = _maxZoomLevel;
        Camera.main.orthographicSize = _zoomLevel;
    }

    private void Move()
    {
        if (_beforeMousePos == Vector3.zero)
            return;

        Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveVec = _beforeMousePos - curMousePos;
        Camera.main.transform.Translate(moveVec);
    }

    public void SelectSpawnObject(Define.ObjectType type)
    {
        MapMakingScene scene = Managers.Scene.GetCurrentScene<MapMakingScene>();
        if (type == scene._curSelectObjectType)
            scene._curSelectObjectType = Define.ObjectType.ObjectEnd;
        else
            scene._curSelectObjectType = type;
    }

    public void SpawnRoom()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        MapMakingScene scene = Managers.Scene.GetCurrentScene<MapMakingScene>();
        if (scene._curSelectObjectType == Define.ObjectType.ObjectEnd)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.red, 5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform == null || (hit.transform.name != "RoomMaking"))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject instance = Managers.Resource.Instantiate(Managers.Scene.GetCurrentScene<MapMakingScene>().GetCurSelectObjectPath());
            instance.transform.position = pos;
        }
    }

    public void SpawnObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        MapMakingScene scene = Managers.Scene.GetCurrentScene<MapMakingScene>();
        if (scene._curSelectObjectType == Define.ObjectType.ObjectEnd)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.red, 5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null && hit.transform.name == "RoomMaking")
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MapMakingScene mapMakingscene = Managers.Scene.GetCurrentScene<MapMakingScene>();
            // 오브젝트 소환
            if (mapMakingscene._curSelectObjectType > Define.ObjectType.Object)
            {
                GameObject instance = Managers.Resource.Instantiate(mapMakingscene.GetCurSelectObjectPath(), hit.transform);
                instance.transform.position = pos;
                MakingObject makingObject = instance.GetComponent<MakingObject>();
                if (makingObject != null)
                    makingObject.parentRoom = hit.transform.gameObject;
            }
            // 몬스터 소환
            else
            {
                GameObject instance = Managers.Resource.Instantiate(mapMakingscene.GetCurSelectObjectPath(), hit.transform);
                instance.transform.position = pos;
                instance.GetComponent<MakingObject>().parentRoom = hit.transform.gameObject;
            }
        }
    }

    public void SelectInstance()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject curInstance = Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance;
            if (curInstance != null)
                curInstance.GetComponentInChildren<InstanceToolMaking>().gameObject.GetComponent<Image>().enabled = false;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null)
        {
            if (!hit.transform.CompareTag("Making"))
                return;

            Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance = hit.transform.gameObject;
        }
    }
}
