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

    public static T LoadJson<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
