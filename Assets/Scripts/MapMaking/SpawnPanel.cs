using UnityEngine;
using UnityEngine.UI;

public class SpawnPanel : UIBase
{
    GameObject MonsterSpawnPanel;
    GameObject ObjectSpawnPanel;
    InputController _inputController;

    Button _curSelectButton;

    [SerializeField] Button _basicEnemyButton;
    [SerializeField] Button _bombEnemyButton;
    [SerializeField] Button _bossEnemyButton;
    [SerializeField] Button _doorHorizontalButton;
    [SerializeField] Button _doorVerticalButton;
    [SerializeField] Button _storeButton;

    private void Start()
    {
        MonsterSpawnPanel = transform.Find("MonsterSpawnPanel").gameObject;
        ObjectSpawnPanel = transform.Find("ObjectSpawnPanel").gameObject;

        _inputController = GameObject.FindObjectOfType<InputController>();
    }

    Button CurSelectButton
    {
        get
        {
            return _curSelectButton;
        }

        set
        {
            if (_curSelectButton != null)
                _curSelectButton.GetComponent<Image>().color = Color.white;

            if (_curSelectButton == value)
            {
                _curSelectButton = null;
            }
            else
            {
                _curSelectButton = value;
                if (_curSelectButton != null)
                    _curSelectButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void OnBasicEnemyButtonClick()
    {
        CurSelectButton = _basicEnemyButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BasicEnemyMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnBombEnemyButtonClick()
    {
        CurSelectButton = _bombEnemyButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BombEnemyMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnBossEnemyButtonClick()
    {
        CurSelectButton = _bossEnemyButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BossEnemyMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnDoorHorizontalButtonClick()
    {
        CurSelectButton = _doorHorizontalButton;
        _inputController.SelectSpawnObject(Define.ObjectType.DoorHorizontalMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnDoorVerticalButtonClick()
    {
        CurSelectButton = _doorVerticalButton;
        _inputController.SelectSpawnObject(Define.ObjectType.DoorVerticalMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnStoreButtonClick()
    {
        CurSelectButton = _storeButton;
        _inputController.SelectSpawnObject(Define.ObjectType.StoreMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void ShowMonsterSpawnPanel()
    {
        MonsterSpawnPanel.SetActive(true);
    }

    public void ShowObjectSpawnPanel()
    {
        ObjectSpawnPanel.SetActive(true);
    }

    public void Hide()
    {
        CurSelectButton = null;
        _inputController.SelectSpawnObject(Define.ObjectType.ObjectEnd);
        MonsterSpawnPanel.SetActive(false);
        ObjectSpawnPanel.SetActive(false);
    }
}
