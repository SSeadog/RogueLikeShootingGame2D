using UnityEngine;

public class CharacterSelectState : GameState
{

}

public class MainInitState : GameState
{
    public override void OnStart()
    {
        Managers.Game.roomManager.LoadRoomData();
        Managers.Game.roomManager.InitRooms();
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
