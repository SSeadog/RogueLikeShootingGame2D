using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : UIBase
{
    private List<Image> _bulletIconList = new List<Image>();
    private int _maxIndex = 0;
    private int _currentIndex = 0;

    enum TMP_Texts
    {
        AmmoText
    }

    enum Images
    {
        WeaponImage
    }

    enum Transforms
    {
        LoadedAmmoUI
    }

    protected override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(TMP_Texts));
        Bind<Image>(typeof(Images));
        Bind<Transform>(typeof(Transforms));

        LoadBullets();
    }

    void LoadBullets()
    {
        WeaponBase weapon = Managers.Game.PlayerWeaponList[0];
        Transform ammoUIRoot = Get<Transform>(Transforms.LoadedAmmoUI.ToString());
        for (int i = 0; i < weapon.FullLoadAmmo; i++)
        {
            GameObject instance = Managers.Resource.LoadUI("Prefabs/UI/SubItem/AmmoUI", ammoUIRoot);
            _bulletIconList.Add(instance.GetComponent<Image>());
        }

        _maxIndex = weapon.FullLoadAmmo;
        _currentIndex = 0;
    }

    public void RemoveBullet()
    {
        _bulletIconList[_currentIndex].enabled = false;
        _currentIndex++;
    }

    public void FillBullet(int loadedAmmo)
    {
        for (int i = _currentIndex - 1; i >= _currentIndex - loadedAmmo; i--)
        {
            _bulletIconList[i].enabled = true;
        }
        _currentIndex = 0;
    }

    void Update()
    {
        if (Managers.Game.PlayerWeaponList == null || Managers.Game.PlayerWeaponList.Count == 0)
            return;

        WeaponBase weapon = Managers.Game.PlayerWeaponList[0];
        Get<TMP_Text>(TMP_Texts.AmmoText.ToString()).text = $"{weapon.CurLoadAmmo}/{weapon.CurAmmo}";
    }
}
