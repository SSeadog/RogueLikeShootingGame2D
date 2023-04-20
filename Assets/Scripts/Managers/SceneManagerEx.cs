using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene currentScene;

    public T GetCurrentScene<T>() where T : BaseScene
    {
        return currentScene as T;
    }

    public void LoadScene(string name)
    {
        Managers.Clear();
        SceneManager.LoadScene(name);
    }

    public void Clear()
    {
        currentScene?.Clear();
        currentScene = null;
    }
}
