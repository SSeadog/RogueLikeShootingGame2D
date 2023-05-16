using System.Collections.Generic;

public class RoomManager
{
    private Data.RoomData _roomData;

    public List<Room> GetRooms()
    {
        return _roomData.rooms;
    }

    public void LoadRoomData()
    {
        _roomData = Managers.Data.RoomData;
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

    public void Clear()
    {
        _roomData?.rooms.Clear();
    }
}
