using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameState currentState;
    int _playerId;
    List<EnemyControllerBase> _spawnedEnemies = new List<EnemyControllerBase>();

    public RoomManager roomManager = new RoomManager();
    public List<WeaponBase> playerWeaponList = new List<WeaponBase>();
    public int gold;
    public int key;
    public int grenade;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public List<EnemyControllerBase> SpawnedEnemies { get { return _spawnedEnemies; } }

    public GameState GetState()
    {
        return currentState;
    }

    public void SetState(GameState state)
    {
        if (currentState != null)
            currentState.OnEnd();
        currentState = state;

        if (currentState != null)
            currentState.OnStart();
    }

    public void AddSpawnedEnemy(EnemyControllerBase enemy)
    {
        _spawnedEnemies.Add(enemy);
    }

    public void RemoveSpawnedEnemy(EnemyControllerBase enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }

    public void OnUpdate()
    {
        currentState?.Action();
    }

    public void Clear()
    {
        playerWeaponList.Clear();
        _spawnedEnemies.Clear();
        gold = 0;
        key = 0;
        grenade = 0;
    }
}

public class GameState
{
    public virtual void OnStart()
    {
        
    }

    public virtual void Action()
    {

    }

    public virtual void OnEnd()
    {

    }
}