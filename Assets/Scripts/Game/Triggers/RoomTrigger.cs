using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] int _roomId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameState gameState = Managers.Game.GetState();
            if (gameState is MainState)
            {
                MainState mainState = (MainState)gameState;
                Room room = mainState.roomController.FindRoom(_roomId);
                room.Found();
            }
        }
    }
}
