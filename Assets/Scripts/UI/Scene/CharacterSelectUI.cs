using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : UIBase
{
    enum GameObjects
    {
        GridPanel
    }

    protected override void Init()
    {
        base.Init();

        Bind(typeof(GameObjects));

        Transform gridPanel = Get<Transform>(GameObjects.GridPanel.ToString());
        
        List<string> keys = new List<string>(Managers.Data.playerStatDict.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            Data.Stat pStat = Managers.Data.playerStatDict[keys[i]];
            GameObject itemInstance = Managers.Resource.Instantiate("Prefabs/UI/SubItem/CharacterItemUI", gridPanel);
            
            CharacterItemUI uI_CharacterItem = itemInstance.GetComponent<CharacterItemUI>();
            uI_CharacterItem.SetInfo(pStat.name);
            uI_CharacterItem.SetEvent((data) => { Debug.Log(data.name + " º±≈√"); Managers.Game.PlayerId = data.id; Managers.Scene.LoadScene("MainScene"); });
        }
    }
}
