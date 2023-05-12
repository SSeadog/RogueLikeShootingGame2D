using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterItemUI : UIBase
{
    [SerializeField] GameObject _itemIcon;
    [SerializeField] GameObject _itemName;

    private string _name;
    private UnityAction<Data.Stat> _action;

    protected override void Init()
    {
        base.Init();

        _itemName.GetComponent<Text>().text = _name;

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
