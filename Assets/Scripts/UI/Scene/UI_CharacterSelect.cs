using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        List<string> keys = new List<string>(Manager.Data.playerStatDict.Keys);
        Data.PlayerStat pStat;

        for (int i = 0; i < keys.Count; i++)
        {
            pStat = Manager.Data.playerStatDict[keys[i]];
            GameObject UI_CharacterItem = Instantiate(UI_CharacterItemOriginal);
            UI_CharacterItem.transform.SetParent(gridPanel);
            UI_CharacterItem.GetComponent<UI_CharacterItem>().SetInfo(pStat.name);
        }
    }
}
