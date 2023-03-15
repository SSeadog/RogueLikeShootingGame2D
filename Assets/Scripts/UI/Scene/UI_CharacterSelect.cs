using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UI_CharacterSelect : UI_Base
{
    enum GameObjects
    {
        GridPanel
    }

    protected override void Init()
    {
        base.Init();

        Bind(typeof(GameObjects));

        GameObject UI_CharacterItemOriginal = Resources.Load<GameObject>("Prefabs/UI/SubItem/UI_CharacterItem");
        Transform gridPanel = Get<Transform>(GameObjects.GridPanel.ToString());
        
        List<string> keys = new List<string>(Managers.Data.playerStatDict.Keys);
        Data.Stat pStat;

        for (int i = 0; i < keys.Count; i++)
        {
            pStat = Managers.Data.playerStatDict[keys[i]];
            GameObject itemInstance = Instantiate(UI_CharacterItemOriginal);
            itemInstance.transform.SetParent(gridPanel);
            
            UI_CharacterItem uI_CharacterItem = itemInstance.GetComponent<UI_CharacterItem>();
            uI_CharacterItem.SetInfo(pStat.name);
            uI_CharacterItem.SetEvent((data) => { Debug.Log(data.name + " º±≈√"); Managers.Game.PlayerId = data.id; Managers.Scene.LoadScene("SampleScene"); });
        }
    }
}
