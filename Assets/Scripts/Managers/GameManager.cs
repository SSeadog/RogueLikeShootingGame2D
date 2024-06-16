using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private int _stageId;
    private int _playerId;
    private int _gold;
    private int _key;
    private int _grenade;
    private int _killCount;
    private float _playTime;
    private GameState _currentState;
    private RoomManager _roomManager;
    private Dictionary<Define.WeaponType, WeaponBase> _playerWeaponDict = new Dictionary<Define.WeaponType, WeaponBase>();
    private List<Define.WeaponType> _playerWeaponList = new List<Define.WeaponType>();
    private Define.WeaponType _curPlayerWeaponType;
    private List<EnemyControllerBase> _spawnedEnemies = new List<EnemyControllerBase>();

    public int StageId { get { return _stageId; } set { _stageId = value; } }
    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int Key { get { return _key; } set { _key = value; } }
    public int Grenade { get { return _grenade; } set { _grenade = value; } }
    public int KillCount { get { return _killCount; } set { _killCount = value; } }
    public float PlayTime { get { return _playTime; } set { _playTime = value; } }
    public RoomManager RoomManager { get { return _roomManager; } set { _roomManager = value; } }
    public List<Define.WeaponType> PlayerWeaponList { get { return _playerWeaponList; } }
    public Dictionary<Define.WeaponType, WeaponBase> PlayerWeaponDict { get { return _playerWeaponDict; } }
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

    public GameObject LoadUI(string path, Transform parent = null)
    {
        GameObject instance = Managers.Resource.Instantiate(path, parent);

        UIBase uIBase = instance.GetComponent<UIBase>();
        if (uIBase != null)
        {
            Managers.Ui.AddUI(uIBase);
        }
        else
        {
            Debug.Log($"{path}�� uI�� UIBase�� �����ϴ�!");
        }

        return instance;
    }

    public void LoadWeapon(Define.WeaponType curWeaponType, Transform parent)
    {
        GameObject weapon = Managers.Resource.Instantiate("Prefabs/Weapons/" + curWeaponType.ToString(), parent);
        _playerWeaponDict.Add(curWeaponType, weapon.GetComponent<WeaponBase>());
        _playerWeaponList.Add(curWeaponType);
        weapon.SetActive(false);
    }

    public WeaponBase SwapWeapon(int index)
    {
        if (index >= _playerWeaponList.Count)
            return null;

        if (_playerWeaponList[index] == _curPlayerWeaponType)
            return null;

        if (_playerWeaponDict.ContainsKey(_playerWeaponList[index]) == false)
            return null;

        if (_curPlayerWeaponType != Define.WeaponType.None)
            _playerWeaponDict[_curPlayerWeaponType].gameObject.SetActive(false);

        _playerWeaponDict[_playerWeaponList[index]].gameObject.SetActive(true);
        _playerWeaponDict[_playerWeaponList[index]].Swap();
        Managers.Ui.GetUI<WeaponInfoPanel>().SetPanel(_playerWeaponList[index]);
        _curPlayerWeaponType = _playerWeaponList[index];

        return _playerWeaponDict[_playerWeaponList[index]];
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

    public void ClearStageData()
    {
        _stageId = 0;
        _playerId = 0;
        _playerWeaponDict.Clear();
        _playerWeaponList.Clear();
        _spawnedEnemies.Clear();
        _roomManager.Clear();
        _curPlayerWeaponType = Define.WeaponType.None;
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