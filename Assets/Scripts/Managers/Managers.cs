using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get { Init(); return _instance; } }
    private static Managers _instance;

    private DataManager _data = new DataManager();
    private GameManager _game = new GameManager();
    private SceneManagerEx _scene = new SceneManagerEx();

    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    private static void Init()
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
            _instance._game.Init();
            _instance._scene.Init();
        }
    }

    public static void Clear()
    {
        _instance._scene.Clear();
        _instance._game.Clear();
    }
}
