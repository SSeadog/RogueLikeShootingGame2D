using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterItemUI : UIBase
{
    enum GameObjects
    {
        ItemIcon,
        ItemName
    }

    string _name;
    UnityAction<Data.Stat> _action;

    protected override void Init()
    {
        base.Init();

        Bind(typeof(GameObjects));

        Transform nameTr = Get<Transform>(GameObjects.ItemName.ToString());
        nameTr.GetComponent<Text>().text = _name;

        Data.Stat pStat = Managers.Data.playerStatDict[_name];
        gameObject.GetComponent<Button>().onClick.AddListener(()=> { _action.Invoke(pStat); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

    public void SetEvent(UnityAction<Data.Stat> action)
    {
        _action = action;
    }
}
