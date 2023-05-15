using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterItemUI : UIBase
{
    enum Texts
    {
        ItemName
    }
    enum GameObjects
    {
        ItemIcon
    }

    private string _name;
    private UnityAction<Data.Stat> _action;

    protected override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Text>(Texts.ItemName.ToString()).text = _name;
        Data.Stat pStat = Managers.Data.PlayerStatDict[_name];
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
