using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected virtual void Init()
    {

    }

    void Start()
    {
        Init();
    }

    public abstract void Clear();
}
