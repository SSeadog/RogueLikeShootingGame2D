using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // 왜 Managers _instance를 static으로 둬야했을까?
    // 왜 다른 매니저들(DataManager, GameManager, SceneManagerEx)는 static으로 두지 않는 걸까?
    // 지금 이대로 사용할 때 그냥 인스턴스를 생성해버리면 무슨 일이 일어날까?
    // a. 유니티에서 막음. 그렇다면 AddComponent로 추가한다면 무슨 일이 일어날까?
    // // a. 생성은 됨. 그래서 일반적으로 클래스에서 사용 가능한 요소들은 사용이 가능할텐데 그 목록은 일반 멤버들이 될 것임
    // // 따라서 싱글톤으로 구현하려는 경우, 일반 멤버를 두면 안됨. static멤버는 인스턴스에서 사용이 불가능함. 그 이유는?
    // 근데 싱글톤을 굳이 static 클래스가 아니라 일반 클래스를 static멤버로 가지는 방식으로 구현하는 이유가 뭐였지?

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

            // 매니저에 연결된 각종 매니저들 초기화
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
