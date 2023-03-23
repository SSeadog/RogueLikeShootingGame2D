using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private GameState currentState;
    private int _playerId;
    private int _gold;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public GameState GetState()
    {
        return currentState;
    }

    public void Init()
    {
        currentState = new CharacterSelectState();
    }

    public void SetState(GameState state)
    {
        if (state != null)
            state.OnEnd();
        currentState = state;

        if (state != null)
            state.OnStart();
    }

    public void Clear()
    {
        //playerId = 0;
        SetState(null);
    }
}

public class GameState
{
    public virtual void OnStart()
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
    public RoomController roomController = new RoomController();

    public override void OnEnd()
    {
        base.OnEnd();

        roomController.Clear();
    }
}

public class RoomController
{
    List<Room> _rooms = new List<Room>();

    public List<Room> GetRooms()
    {
        return _rooms;
    }

    public Room FindRoom(int id)
    {
        foreach (Room room in _rooms)
        {
            if (id == room.roomId)
                return room;
        }

        return null;
    }

    public void Add(Room room)
    {
        _rooms.Add(room);
    }

    public void Clear()
    {
        _rooms.Clear();
    }
}