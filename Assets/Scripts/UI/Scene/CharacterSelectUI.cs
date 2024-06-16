using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : UIBase
{
    enum Transforms
    {
        Content
    }

    protected override void Init()
    {
        base.Init();
        
        Bind<Transform>(typeof(Transforms));

        List<string> keys = new List<string>(Managers.Data.PlayerStatDict.Keys);

        Transform gridPanel = Get<Transform>(Transforms.Content.ToString());
        for (int i = 0; i < keys.Count; i++)
        {
            Data.PlayerStat pStat = Managers.Data.PlayerStatDict[keys[i]];
            GameObject itemInstance = Managers.Resource.Instantiate("Prefabs/UI/SubItem/CharacterItemUI", gridPanel);
            
            CharacterItemUI uI_CharacterItem = itemInstance.GetComponent<CharacterItemUI>();
            uI_CharacterItem.SetInfo(pStat);
            // uI_CharacterItem.SetEvent((data) => { Managers.Game.PlayerId = data.id; Managers.Scene.LoadScene("MainScene"); });
            uI_CharacterItem.SetEvent((stat) => OnButtonClicked(stat));
        }
    }

    private void OnButtonClicked(Data.PlayerStat stat) {
        Managers.Game.PlayerId = stat.id;

        gameObject.SetActive(false);
        Managers.Ui.GetUI<StageSelectUI>().gameObject.SetActive(true);
    }
}
