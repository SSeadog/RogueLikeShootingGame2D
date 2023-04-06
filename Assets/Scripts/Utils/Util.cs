using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
            case Define.ObjectType.Bomb:
                break;
            case Define.ObjectType.BossEnemy:
                break;
        }

        return null;
    }

    #region ReadJson
    public static T LoadJson<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
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
}
