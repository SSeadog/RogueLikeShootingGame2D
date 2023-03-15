using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private int _playerId;
    private int _gold;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public List<Room> rooms = new List<Room>();

    public void Init()
    {

    }

    public Room FindRoom(int id)
    {
        foreach(Room room in rooms)
        {
            if (id == room.roomId)
                return room;
        }

        return null;
    }

    public void Clear()
    {
        rooms.Clear();
        //playerId = 0;
    }
}
