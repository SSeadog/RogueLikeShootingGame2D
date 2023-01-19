using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_CharacterItem : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemName
    }

    string _name;
    UnityAction<Data.PlayerStat> _action;
    Data.PlayerStat _param;

    protected override void Init()
    {
        base.Init();

        Bind(typeof(GameObjects));

        Transform nameTr = Get<Transform>(GameObjects.ItemName.ToString());
        nameTr.GetComponent<Text>().text = _name;

        Data.PlayerStat pStat = Managers.Data.playerStatDict[_name];
        gameObject.GetComponent<Button>().onClick.AddListener(()=> { _action.Invoke(pStat); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

    public void SetEvent(UnityAction<Data.PlayerStat> action)
    {
        _action = action;
    }
}
