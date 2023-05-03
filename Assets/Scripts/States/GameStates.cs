using UnityEngine;

public class CharacterSelectState : GameState
{

}

public class MainInitState : GameState
{
    public override void OnStart()
    {
        Managers.Game.RoomManager.LoadRoomData();
        Managers.Game.RoomManager.InitRooms();
        Managers.Game.SetState(new MainState());
    }
}

public class MainState : GameState
{
}

public class RoomEnterState : GameState
{
    public Room _room;

    public void SetRoom(Room room)
    {
        _room = room;
    }

    public override void Action()
    {
        if (Managers.Game.SpawnedEnemies.Count == 0)
        {
            _room.OpenDoors();
            Managers.Game.SetState(new MainState());
        }
    }
}

public class MainEndState : GameState
{
    private bool _isWin = false;
    private float _stopTime = 2f;
    private float _stopTimer = 0f;
    private bool _isStop = false;

    public bool IsWin { get { return _isWin; } set { _isWin = value; } }

    public override void OnStart()
    {
        GameEndingPanel gameEndingPanel = Managers.Ui.GetUI<GameEndingPanel>();
        gameEndingPanel.Show(_isWin, _stopTime);
    }

    public override void Action()
    {
        // 타이머 두고 2초 정도 기다리기
        if (_stopTimer < _stopTime)
        {
            _stopTimer += Time.deltaTime;
        }
        else if (_stopTimer > _stopTime)
        {
            if (_isStop == false)
            {
                _isStop = true;
                Time.timeScale = 0f;
            }
        }
    }

    public override void OnEnd()
    {
        Time.timeScale = 1f;
    }
}
