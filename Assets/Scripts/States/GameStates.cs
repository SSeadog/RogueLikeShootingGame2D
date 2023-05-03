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
    float _stopTime = 2f;
    float _stopTimer = 0f;
    bool _isStop = false;

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
                //Time.timeScale = 0f;
                GameEndingPanel gameEndingPanel = Managers.Ui.GetUI<GameEndingPanel>();
                gameEndingPanel.Show();
            }
        }
    }

    public override void OnEnd()
    {
        Time.timeScale = 1f;
    }
}
