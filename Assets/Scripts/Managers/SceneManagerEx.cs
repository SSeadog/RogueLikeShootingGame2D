using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    GameObject _scene;

    public void Init()
    {
        _scene = GameObject.Find("@Scene");
        if (_scene == null)
        {
            _scene = new GameObject("@Scene");
            string sceneName = SceneManager.GetActiveScene().name;
            Type type = Type.GetType(sceneName);
            _scene.AddComponent(type);
        }
    }

    public void Clear()
    {
        _scene = null;
    }
}
