using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : UIBase
{
    [SerializeField] GameObject _gridPanel;

    enum GameObjects
    {
        GridPanel
    }

    protected override void Init()
    {
        base.Init();
        
        List<string> keys = new List<string>(Managers.Data.PlayerStatDict.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            Data.Stat pStat = Managers.Data.PlayerStatDict[keys[i]];
            GameObject itemInstance = Managers.Resource.Instantiate("Prefabs/UI/SubItem/CharacterItemUI", _gridPanel.transform);
            
            CharacterItemUI uI_CharacterItem = itemInstance.GetComponent<CharacterItemUI>();
            uI_CharacterItem.SetInfo(pStat.name);
            uI_CharacterItem.SetEvent((data) => { Managers.Game.PlayerId = data.id; Managers.Scene.LoadScene("MainScene"); });
        }
    }
}
