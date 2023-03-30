using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 특정 state에서만 동작하는 건 state 안으로 밀어넣기
    // 이때 GameState를 받지말고 자식으로 넘겨받을 순 없나? 쓰면서 변환하기 너무 번거로운데
    // RoomController같은 경우 어떻게 관리할지 다시 생각해보기
    private GameState currentState;

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
        roomController.Clear();
    }}

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