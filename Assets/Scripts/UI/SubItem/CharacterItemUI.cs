using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterItemUI : UIBase
{
    enum Texts
    {
        CharacterName,
        WeaponName
    }
    enum Images
    {
        CharacterImage,
        WeaponImage
    }

    private Data.PlayerStat _pStat;
    private UnityAction<Data.PlayerStat> _action;

    protected override void Init()
    {
        base.Init();

        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        string weaponName = ((Define.WeaponType)_pStat.weaponId).ToString();

        Get<Text>(Texts.CharacterName.ToString()).text = _pStat.name;
        Get<Text>(Texts.WeaponName.ToString()).text = weaponName;
        Get<Image>(Images.CharacterImage.ToString()).sprite = Managers.Resource.Load<Sprite>(_pStat.thumbnailPath);
        Get<Image>(Images.WeaponImage.ToString()).sprite = Managers.Resource.Load<Sprite>(Managers.Data.WeaponDict[weaponName].thumbnailPath);
        gameObject.GetComponent<Button>().onClick.AddListener(()=> { _action.Invoke(_pStat); });
    }

    public void SetInfo(Data.PlayerStat pStat)
    {
        _pStat = pStat;
    }

    public void SetEvent(UnityAction<Data.PlayerStat> action)
    {
        _action = action;
    }
}
