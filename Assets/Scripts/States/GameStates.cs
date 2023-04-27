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
