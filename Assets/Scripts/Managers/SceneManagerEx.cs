using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void Init(string name)
    {
        Managers.Clear();
        SceneManager.LoadScene(name);
    }

    public void LoadScene(string name)
    {
        Managers.Clear();
        Init(name);
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
