using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private string _roomName;

    public void SetRoomName(string roomName)
    {
        _roomName = roomName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomEnterState state = new RoomEnterState();
            state.SetRoom(Managers.Game.RoomManager.FindRoom(_roomName));
            Managers.Game.SetState(state);
        }
    }
}
