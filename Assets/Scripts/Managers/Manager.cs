using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get { Init(); return _instance; } }
    private static Manager _instance;

    private float _gameTime = 0f;

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
        }
    }

    void Start()
    {
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
    }

    public float GetGameTime()
    {
        return _gameTime;
    }
}
