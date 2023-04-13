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
        // �� ���� Trigger���� �����ؼ� Trigger�����صα�
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

    // !!�׽�Ʈ �ڵ�!!
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
        // ���� ��� �� 1�� ���� ��ٷȴٰ� UI �ٿ�� ȿ�� ���� ���
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
    // ���� ����� ���� ������ �Ǵ� ���� �����ϴ� ����
    // �ϴ��� ���� �ϳ��⿡ ���� �����ؼ� MapMakingState�� �ѱ�
    public override void OnStart()
    {
        Managers.Game.SetState(new MapInstanceSelectState());
    }
}

// �ν��Ͻ� ��ȯ ���
public class MapRoomSpawnState : GameState
{
    // ���� ��ġ ��ҵ� ��ġ�� �� �ִ� ����
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetRoomSpawnMouseEvent();
    }
}

public class MapObjectSpawnState : GameState
{
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetObjectSpawnMouseEvent();
    }
}

public class MapSpawnState : GameState
{
    // ���� ��ġ ��ҵ� ��ġ�� �� �ִ� ����
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetObjectSpawnMouseEvent();
    }
}

// ��ȯ�� �ν��Ͻ� ���� ���
public class MapInstanceSelectState : GameState
{
    public override void OnStart()
    {
        (Managers.Scene.currentScene as MapMakingScene)._inputController.SetInstanceSelectMouseEvenet();
    }
}

// ������ �ν��Ͻ� ���� ���
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
    // ��ġ�� �Ϸ��ϰ� �����ϴ� ����. �� ���´� �ʿ� ���� ���ɼ��� ����
}
#endregion