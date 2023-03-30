using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject uiRoot = null;
    private List<UIBase> uiList = new List<UIBase>();

    public void AddUI(UIBase ui)
    {
        uiList.Add(ui);
    }

    public List<UIBase> GetUIList()
    {
        return uiList;
    }

    public T GetUI<T>() where T : UIBase
    {
        foreach (UIBase ui in uiList)
        {
            if (ui is T)
                return (T)ui;
        }

        Debug.Log($"UI를 찾을 수 없습니다!");
        return null;
    }

    public void Clear()
    {
        uiList.Clear();
    }
}
