using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager
{
    private int _playerId;
    private int _gold;
    private int _key;
    private int _grenade;
    private int _killCount;
    private float _playTime;
    private GameState _currentState;
    private RoomManager _roomManager = new RoomManager();
    private List<WeaponBase> _playerWeaponList = new List<WeaponBase>();
    private List<EnemyControllerBase> _spawnedEnemies = new List<EnemyControllerBase>();

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int Key { get { return _key; } set { _key = value; } }
    public int Grenade { get { return _grenade; } set { _grenade = value; } }
    public int KillCount { get { return _killCount; } set { _killCount = value; } }
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    public RoomManager RoomManager { get { return _roomManager; } }
    public List<WeaponBase> PlayerWeaponList { get { return _playerWeaponList; } }
    public List<EnemyControllerBase> SpawnedEnemies { get { return _spawnedEnemies; } }

    public GameState GetState()
    {
        return _currentState;
    }

    public void SetState(GameState state)
    {
        if (_currentState != null)
            _currentState.OnEnd();
        _currentState = state;

        if (_currentState != null)
            _currentState.OnStart();
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
        _currentState?.Action();
    }

    public void Clear()
    {
        _playerWeaponList.Clear();
        _spawnedEnemies.Clear();
        _roomManager.Clear();
        _gold = 0;
        _key = 0;
        _grenade = 0;
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