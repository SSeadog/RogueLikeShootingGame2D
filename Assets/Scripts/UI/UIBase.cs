using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    Dictionary<Type, Dictionary<string, UnityEngine.Object>> objects = new Dictionary<Type, Dictionary<string, UnityEngine.Object>>();

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Managers.Ui.AddUI(this);
    }

    protected void Bind(Type enumType)
    {
        string[] names = enumType.GetEnumNames();

        Dictionary<string, UnityEngine.Object> _objects = new Dictionary<string, UnityEngine.Object>();
        objects.Add(typeof(Transform), _objects);

        foreach (string name in names)
        {
            foreach (Transform component in gameObject.GetComponentInChildren<Transform>())
            {
                if (name == component.name)
                {
                    _objects[name] = component;
                }
            }

            if (_objects.ContainsKey(name) == false)
            {
                Debug.Log(name + ": Bind ½ÇÆÐ");
            }
        }
    }

    protected T Get<T>(string name) where T : UnityEngine.Object
    {
        T go = objects[typeof(T)][name] as T;

        return go;
    }
}
