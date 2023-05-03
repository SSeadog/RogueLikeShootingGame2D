using UnityEngine;
using UnityEngine.UI;

public class SpawnPanel : UIBase
{
    GameObject MonsterSpawnPanel;
    GameObject ObjectSpawnPanel;
    InputController _inputController;

    Button _curSelectButton;

    [SerializeField] Button _testEnemyButton;
    [SerializeField] Button _testBombButton;
    [SerializeField] Button _testBossButton;
    [SerializeField] Button _doorHorizontalButton;
    [SerializeField] Button _doorVerticalButton;
    [SerializeField] Button _storeButton;
    [SerializeField] Button _tableButton;

    private void Start()
    {
        MonsterSpawnPanel = transform.Find("MonsterSpawnPanel").gameObject;
        ObjectSpawnPanel = transform.Find("ObjectSpawnPanel").gameObject;

        _inputController = GameObject.FindObjectOfType<InputController>();
    }

    // 각 버튼 클릭하면 소환물로 등록해두고
    // 버튼 클릭됐다는 표시해주기
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

    public void OnTestEnemyButtonClick()
    {
        CurSelectButton = _testEnemyButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BasicEnemyMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnTestBombEnemyButtonClick()
    {
        CurSelectButton = _testBombButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BombEnemyMaking);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnTestBossEnemyButtonClick()
    {
        CurSelectButton = _testBossButton;
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

    public void OnTableButtonClick()
    {
        CurSelectButton = _tableButton;
        Debug.Log("테이블 선택(아직 제작 안함)");
        //_inputController.SelectSpawnObject(Define.ObjectType.Table);
        //Managers.Game.SetState(new MapObjectSpawnState());
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
