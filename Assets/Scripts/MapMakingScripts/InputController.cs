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

    float _orthographicSize;
    float _scrollSpeed;
    float _moveSpeed;

    Action mouseLeftButtonClick;
    Action mouseWheelDown;
    Action mouseWheelUp;
    Action mouseWheelButtonClick;

    void Start()
    {
        _orthographicSize = Camera.main.orthographicSize;
        _scrollSpeed = 1f;
        _moveSpeed = 0.5f;

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

        if (Input.GetMouseButton(2))
        {
            mouseWheelButtonClick();
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

    private void EnLarge()
    {
        _orthographicSize -= _scrollSpeed;
        if (_orthographicSize < 1f)
            _orthographicSize = 1f;
        Camera.main.orthographicSize = _orthographicSize;
    }

    private void Reduce()
    {
        _orthographicSize += _scrollSpeed;
        Camera.main.orthographicSize = _orthographicSize;
    }

    private void Move()
    {
        float movX = Input.GetAxis("Mouse X");
        float movY = Input.GetAxis("Mouse Y");

        Vector3 movVec = new Vector3(-movX, -movY, 0f) * _moveSpeed;
        Camera.main.transform.Translate(movVec);
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
