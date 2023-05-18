using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    // GameObject, Image, Text 등의 UI요소들을 저장할 Dictionary
    Dictionary<Type, Dictionary<string, UnityEngine.Object>> _objects = new Dictionary<Type, Dictionary<string, UnityEngine.Object>>();

    void Awake()
    {
        InitImediately();
    }
    
    void Start()
    {
        Init();
    }

    protected virtual void InitImediately()
    {

    }

    protected virtual void Init()
    {
    }

    protected void Bind<T>(Type enumType) where T : UnityEngine.Object
    {
        if (_objects.ContainsKey(typeof(T)) == false)
        {
            Dictionary<string, UnityEngine.Object> objects = new Dictionary<string, UnityEngine.Object>();
            _objects.Add(typeof(T), objects);
        }

        string[] names = enumType.GetEnumNames();

        foreach (string name in names)
        {
            T bindObject = null;

            if (typeof(T) == typeof(GameObject))
                bindObject = Util.FindChild(gameObject, name) as T;
            else
                bindObject = Util.FindChild<T>(gameObject, name);

            if (bindObject != null)
                _objects[typeof(T)].Add(name, bindObject);
        }
    }

    protected T Get<T>(string name) where T : UnityEngine.Object
    {
        return _objects[typeof(T)][name] as T;
    }
}
