using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : UIBase
{
    private List<Image> _bulletIconList = new List<Image>();
    private int _maxIndex = 0;
    private int _currentIndex = 0;
    private Define.WeaponType _curWeaponType;

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

    protected override void InitImediately()
    {
        base.InitImediately();

        Bind<TMP_Text>(typeof(TMP_Texts));
        Bind<Image>(typeof(Images));
        Bind<Transform>(typeof(Transforms));
    }

    public void SetPanel(Define.WeaponType weaponType)
    {
        _curWeaponType = weaponType;
        LoadThumbImage();
        LoadBullets();
    }

    void LoadThumbImage()
    {
        string thumbPath = Managers.Data.WeaponDict[_curWeaponType.ToString()].thumbnailPath;
        Sprite image = Managers.Resource.Load<Sprite>(thumbPath);
        Get<Image>(Images.WeaponImage.ToString()).sprite = image;
    }

    void LoadBullets()
    {
        _currentIndex = 0;
        WeaponBase weapon = Managers.Game.PlayerWeaponDict[_curWeaponType];
        Transform ammoUIRoot = Get<Transform>(Transforms.LoadedAmmoUI.ToString());

        for (int i = 0; i < _bulletIconList.Count; i++)
        {
            Destroy(_bulletIconList[i].gameObject);
        }
        _bulletIconList.Clear();

        for (int i = 0; i < weapon.FullLoadAmmo; i++)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/UI/SubItem/AmmoUI", ammoUIRoot);
            _bulletIconList.Add(instance.GetComponent<Image>());
        }

        for (int i = 0; i < weapon.FullLoadAmmo - weapon.CurLoadAmmo; i++)
        {
            RemoveBullet();
        }

        _maxIndex = weapon.FullLoadAmmo;
    }

    public void RemoveBullet()
    {
        if (_currentIndex >= _bulletIconList.Count)
            return;

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
        if (Managers.Game.PlayerWeaponDict == null || Managers.Game.PlayerWeaponDict.Count == 0)
            return;

        WeaponBase weapon = Managers.Game.PlayerWeaponDict[_curWeaponType];
        Get<TMP_Text>(TMP_Texts.AmmoText.ToString()).text = $"{weapon.CurLoadAmmo}/{weapon.CurAmmo}";
    }
}
