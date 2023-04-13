using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private GameState currentState;

    public RoomController roomController = new RoomController();

    private int _playerId;
    public List<WeaponBase> playerWeaponList = new List<WeaponBase>();
    public int gold;
    public int key;
    public int grenade;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }

    public GameState GetState()
    {
        return currentState;
    }

    public void SetState(GameState state)
    {
        if (currentState != null)
            currentState.OnEnd();
        currentState = state;

        if (currentState != null)
            currentState.OnStart();
    }

    public void OnUpdate()
    {
        currentState?.Action();
    }

    public void Clear()
    {
        playerWeaponList.Clear();
        gold = 0;
        key = 0;
        grenade = 0;
    }
}

public class GameState
{
    public virtual void OnStart()
    {
        
    }

    public virtual void Action()
    {

    }

    public virtual void OnEnd()
    {

    }
}

public class CharacterSelectState : GameState
{

}

public class MainState : GameState
{
    public override void OnStart()
    {
        Managers.Game.roomController.LoadRoomData();
        //RoomEnterState state = new RoomEnterState();
        //state.SetRoom(Managers.Game.roomController.FindRoom("StartRoom"));
        //Managers.Game.SetState(state);
        // 각 룸의 Trigger인포 참조해서 Trigger세팅해두기
        Managers.Game.roomController.InitRooms();
    }
}

public class RoomEnterState : GameState
{
    public Room _room;

    public void SetRoom(Room room)
    {
        _room = room;
    }

    // !!테스트 코드!!
    public override void Action()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _room.OpenDoors();
            Managers.Game.SetState(new RoomClearState());
        }
    }
}

public class RoomClearState : GameState
{

}

public class MainEndState : GameState
{
    public override void OnStart()
    {
        // 몬스터 사망 등 1초 정도 기다렸다가 UI 뛰우는 효과 줄지 고민
        Time.timeScale = 0f;
        GameEndingPanel gameEndingPanel = Managers.Ui.GetUI<GameEndingPanel>();
        gameEndingPanel.Show();
    }

    public override void OnEnd()
    {
        Time.timeScale = 1f;
    }
}

public class RoomController
{
    Define.RoomData _roomData;
    Room _currentRoom;

    public List<Room> GetRooms()
    {
        return _roomData.rooms;
    }

    public void LoadRoomData()
    {
        _roomData = Managers.Data.roomData;
    }

    public void SetCurrentRoom(Room room)
    {
        _currentRoom = room;
    }

    public void InitRooms()
    {
        foreach (Room room in _roomData.rooms)
        {
            room.Init();
        }
    }

    public Room FindRoom(string name)
    {
        foreach (Room room in _roomData.rooms)
        {
            if (name == room.name)
                return room;
        }

        return null;
    }

    public void Add(Room room)
    {
        _roomData.rooms.Add(room);
    }

    public void Clear()
    {
        _roomData.rooms.Clear();
    }
}

#region MapMakingState
public class MapSelectState : GameState
{
    // 맵을 만들기 위해 기준이 되는 맵을 선택하는 상태
    // 일단은 맵이 하나기에 맵을 선택해서 MapMakingState로 넘김
    public override void OnStart()
    {
        Managers.Game.SetState(new MapInstanceSelectState());
    }
}

// 인스턴스 소환 모드
public class MapRoomSpawnState : GameState
{
    // 각종 배치 요소들 배치할 수 있는 상태
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetRoomSpawnMouseEvent();
    }
}

public class MapObjectSpawnState : GameState
{
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetObjectSpawnMouseEvent();
    }
}

public class MapSpawnState : GameState
{
    // 각종 배치 요소들 배치할 수 있는 상태
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetObjectSpawnMouseEvent();
    }
}

// 소환한 인스턴스 선택 모드
public class MapInstanceSelectState : GameState
{
    public override void OnStart()
    {
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetInstanceSelectMouseEvenet();
    }
}

// 선택한 인스턴스 수정 모드
public class MapInstanceEditState : GameState
{
    public override void OnStart()
    {
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetInstanceEditMouseEvent();
    }

    public override void OnEnd()
    {
        (Managers.Scene.currentScene as MapMakingScene).CurSelectInstance = null;
    }
}

public class MapMakingDoneState : GameState
{
    // 배치를 완료하고 저장하는 상태. 이 상태는 필요 없을 가능성도 있음
}
#endregion