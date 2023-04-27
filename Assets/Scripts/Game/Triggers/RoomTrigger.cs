using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] string _roomName;

    public void SetRoomName(string roomName)
    {
        _roomName = roomName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Room room = Managers.Game.roomManager.FindRoom(_roomName);
            room.Found();
        }
    }
}
