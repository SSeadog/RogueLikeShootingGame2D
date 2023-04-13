using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected virtual void Init()
    {
        Managers.Scene.currentScene = this;
    }

    void Start()
    {
        Init();
    }

    public abstract void Clear();
}
