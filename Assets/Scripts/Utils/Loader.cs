using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Loader
{
    public static T LoadJson<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
