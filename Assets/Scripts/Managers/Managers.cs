using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get { Init(); return _instance; } }
    static Managers _instance;

    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager Ui { get { return Instance._ui; } }

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

            // 매니저에 연결된 각종 매니저들 초기화
            _instance._data.Init();
        }
    }

    public static void Clear()
    {
        _instance._game.Clear();
        _instance._resource.Clear();
        _instance._ui.Clear();
        _instance._scene.Clear();
    }
}
