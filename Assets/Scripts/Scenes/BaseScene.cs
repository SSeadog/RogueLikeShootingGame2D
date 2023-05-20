using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected virtual void Init()
    {
        Managers.Scene.CurrentScene = this;
        Managers.Ui.Init();
    }

    void Start()
    {
        Init();
    }

    public virtual void Clear()
    {
        Managers.Ui.Clear();
    }
}
