using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // �� Managers _instance�� static���� �־�������?
    // �� �ٸ� �Ŵ�����(DataManager, GameManager, SceneManagerEx)�� static���� ���� �ʴ� �ɱ�?
    // ���� �̴�� ����� �� �׳� �ν��Ͻ��� �����ع����� ���� ���� �Ͼ��?
    // a. ����Ƽ���� ����. �׷��ٸ� AddComponent�� �߰��Ѵٸ� ���� ���� �Ͼ��?
    // // a. ������ ��. �׷��� �Ϲ������� Ŭ�������� ��� ������ ��ҵ��� ����� �������ٵ� �� ����� �Ϲ� ������� �� ����
    // // ���� �̱������� �����Ϸ��� ���, �Ϲ� ����� �θ� �ȵ�. static����� �ν��Ͻ����� ����� �Ұ�����. �� ������?
    // �ٵ� �̱����� ���� static Ŭ������ �ƴ϶� �Ϲ� Ŭ������ static����� ������ ������� �����ϴ� ������ ������?

    public static Managers Instance { get { Init(); return _instance; } }
    static Managers _instance;

    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            Managers mg;
            if (go == null)
            {
                go = new GameObject("@Managers");
                mg = go.AddComponent<Managers>();
            }
            else
            {
                mg = go.GetComponent<Managers>();
                if (mg == null)
                {
                    mg = go.AddComponent<Managers>();
                }
            }

            _instance = mg;
            DontDestroyOnLoad(go);

            // �Ŵ����� ����� ���� �Ŵ����� �ʱ�ȭ
            _instance._data.Init();
            _instance._game.Init();
        }
    }

    public static void Clear()
    {
        _instance._game.Clear();
        _instance._resource.Clear();
        _instance._scene.Clear();
    }
}
