using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : UIBase
{
    enum TMP_Texts
    {
        GoldText,
        KeyText
    }

    enum Transforms
    {
        GrenadeUI
    }

    private List<Image> _hpIcons = new List<Image>();
    private GameObject _grenadeIcon;
    private List<GameObject> _grenadeIcons = new List<GameObject>();
    private int _curCount = 0;

    protected override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(TMP_Texts));
        Bind<Transform>(typeof(Transforms));
        _grenadeIcon = Managers.Resource.Load("Prefabs/UI/SubItem/GrenadeIconUI");
        SetGrenadeCount(Managers.Game.Grenade);
    }

    public void SetHpBar(int hp)
    {
        if (_hpIcons.Count == 0)
            InitHpBar();
        PaintHpBar(hp);
    }

    public void SetGrenadeCount(int count)
    {
        // 많으면 추가
        if (count > _curCount)
        {
            Transform grenadeIconRoot = Get<Transform>(Transforms.GrenadeUI.ToString());
            int addCount = count - _curCount;
            for (int i = 0; i < addCount; i++)
            {
                GameObject instance = Instantiate(_grenadeIcon, grenadeIconRoot);
                _grenadeIcons.Add(instance);
            }
        }
        // 적으면 삭제
        else if (count < _curCount)
        {
            int removeCount = _curCount - count;
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(_grenadeIcons[0]);
                _grenadeIcons.RemoveAt(0);
            }
        }
        _curCount = count;
    }

    void InitHpBar()
    {
        List<Image> images = new List<Image>(gameObject.GetComponentsInChildren<Image>());
        for (int i = 0; i < images.Count; i++)
        {
            if (images[i].name == "HeartIcon")
                _hpIcons.Add(images[i]);
        }
    }

    void PaintHpBar(int hp)
    {
        if (hp < 0)
            return;

        for (int i = 0; i < hp; i++)
        {
            _hpIcons[i].enabled = true;
        }
        for (int i = hp; i < 10; i++)
        {
            _hpIcons[i].enabled = false;
        }
    }

    void Update()
    {
        Get<TMP_Text>(TMP_Texts.KeyText.ToString()).text = Managers.Game.Key.ToString();
        Get<TMP_Text>(TMP_Texts.GoldText.ToString()).text = Managers.Game.Gold.ToString();
        SetGrenadeCount(Managers.Game.Grenade);
    }
}
