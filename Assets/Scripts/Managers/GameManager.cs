using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // Ư�� state������ �����ϴ� �� state ������ �о�ֱ�
    // �̶� GameState�� �������� �ڽ����� �Ѱܹ��� �� ����? ���鼭 ��ȯ�ϱ� �ʹ� ���ŷο
    // RoomController���� ��� ��� �������� �ٽ� �����غ���
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