using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    public static string GetMonsterPrefabPath(Define.ObjectType type)
    {
        switch (type)
        {
            case Define.ObjectType.Monster:
                break;
            case Define.ObjectType.Hyena:
                break;
            case Define.ObjectType.TestBombEnemy:
                break;
            case Define.ObjectType.BossEnemy:
                break;
        }

        return null;
    }

    public static Define.ObjectType ConvertMakingTypeToObjectType(Define.ObjectType type)
    {
        string makingTypeName = type.ToString();
        string typeName = makingTypeName.Remove(makingTypeName.IndexOf("Making"));

        return Enum.Parse<Define.ObjectType>(typeName);
    }

    public static Define.ObjectType ConvertObjectTypeToMakingType(Define.ObjectType type)
    {
        string makingTypeName = type.ToString() + "Making";
        return Enum.Parse<Define.ObjectType>(makingTypeName);
    }

    #region ReadJson
    public static T LoadJson<T>(string path)
    {
        string text = "";
        if (path.IndexOf(".json") == -1)
        {
            text = Resources.Load<TextAsset>(path).text;
        }
        else
        {
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
        }
        
        return JsonConvert.DeserializeObject<T>(text);
    }

    public static T LoadJsonList<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<T>(textAsset.text);
    }

    public static Dictionary<T, T2> LoadJsonDict<T, T2>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<Dictionary<T, T2>>(textAsset.text);
    }
    #endregion

    public static string ToJson<T>(T data)
    {
        return JsonConvert.SerializeObject(data);
    }
}
