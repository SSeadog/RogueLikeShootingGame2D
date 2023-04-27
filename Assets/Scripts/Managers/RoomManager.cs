using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager
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
