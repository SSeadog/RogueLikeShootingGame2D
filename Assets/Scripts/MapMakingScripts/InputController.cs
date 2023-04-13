using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Input관련 작업을 InputManager로 두고 할지는 고민해봐야할 듯?
// 생각보다 할 일이 조금 있을 거 같음
public class InputController : MonoBehaviour
{
    private float _orthographicSize;
    private float _scrollSpeed;
    private float _moveSpeed;

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
        // 인스턴스 선택하는 함수 등록하기
        mouseLeftButtonClick += SelectInstance;
    }

    public void SetInstanceEditMouseEvent()
    {
        ClearEventsByState();
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
            mouseLeftButtonClick?.Invoke();
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
        MapMakingScene scene = (Managers.Scene.currentScene as MapMakingScene);
        if (type == scene._curSelectObjectType)
            scene._curSelectObjectType = Define.ObjectType.ObjectEnd;
        else
            scene._curSelectObjectType = type;
    }

    public void SpawnRoom()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI를 클릭했습니다");
            return;
        }

        MapMakingScene scene = Managers.Scene.currentScene as MapMakingScene;
        if (scene._curSelectObjectType == Define.ObjectType.ObjectEnd)
        {
            Debug.Log("선택된 오브젝트가 없습니다!");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.red, 5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform == null || (hit.transform.name != "RoomMaking"))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject instance = Managers.Resource.Instantiate((Managers.Scene.currentScene as MapMakingScene).GetCurSelectObjectPath());
            instance.transform.position = pos;
        }
    }

    public void SpawnObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI를 클릭했습니다");
            return;
        }

        MapMakingScene scene = Managers.Scene.currentScene as MapMakingScene;
        if (scene._curSelectObjectType == Define.ObjectType.ObjectEnd)
        {
            Debug.Log("선택된 오브젝트가 없습니다!");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.red, 5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.transform != null && hit.transform.name == "RoomMaking")
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject instance = Managers.Resource.Instantiate((Managers.Scene.currentScene as MapMakingScene).GetCurSelectObjectPath(), hit.transform);
            instance.transform.position = pos;
        }
    }

    public void SelectInstance()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI를 클릭했습니다");
            return;
        }

        Debug.Log("인스턴스 선택 모드!!");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 5f);
        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);
            (Managers.Scene.currentScene as MapMakingScene).CurSelectInstance = hit.transform.gameObject;
            Managers.Game.SetState(new MapInstanceEditState());
        }
    }
}
