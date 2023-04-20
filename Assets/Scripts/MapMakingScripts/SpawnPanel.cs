using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        MonsterSpawnPanel = transform.Find("MonsterSpawnPanel").gameObject;
        ObjectSpawnPanel = transform.Find("ObjectSpawnPanel").gameObject;

        _inputController = GameObject.FindObjectOfType<InputController>();
    }

    // �� ��ư Ŭ���ϸ� ��ȯ���� ����صΰ�
    // ��ư Ŭ���ƴٴ� ǥ�����ֱ�
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
                _curSelectButton.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void OnTestEnemyButtonClick()
    {
        CurSelectButton = _testEnemyButton;
        _inputController.SelectSpawnObject(Define.ObjectType.TestEnemy);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnTestBombEnemyButtonClick()
    {
        CurSelectButton = _testBombButton;
        _inputController.SelectSpawnObject(Define.ObjectType.TestBombEnemy);
        Managers.Game.SetState(new MapObjectSpawnState());
    }

    public void OnTestBossEnemyButtonClick()
    {
        CurSelectButton = _testBossButton;
        _inputController.SelectSpawnObject(Define.ObjectType.BossEnemy);
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
        MonsterSpawnPanel.SetActive(false);
        ObjectSpawnPanel.SetActive(false);
    }
}
