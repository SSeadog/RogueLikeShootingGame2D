using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get { Init(); return _instance; } }
    private static Manager _instance;

    private DataManager _data = new DataManager();

    public static DataManager Data { get { return Instance._data; } }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            Manager mg;
            if (go == null)
            {
                go = new GameObject("@Manager");
                mg = go.AddComponent<Manager>();
            }
            else
            {
                mg = go.GetComponent<Manager>();
                if (mg == null)
                {
                    mg = go.AddComponent<Manager>();
                }
            }

            _instance = mg;
            DontDestroyOnLoad(go);

            // 매니저에 연결된 각종 매니저들 초기화
            _instance._data.Init();
        }
    }
}
