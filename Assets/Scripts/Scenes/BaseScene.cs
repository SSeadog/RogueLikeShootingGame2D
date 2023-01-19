using System.Collections;
using System.Collections.Generic;
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
}
