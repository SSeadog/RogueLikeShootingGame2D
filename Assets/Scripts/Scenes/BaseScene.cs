using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected virtual void Init()
    {
        Managers.Scene.CurrentScene = this;
    }

    void Start()
    {
        Init();
    }

    public abstract void Clear();
}
