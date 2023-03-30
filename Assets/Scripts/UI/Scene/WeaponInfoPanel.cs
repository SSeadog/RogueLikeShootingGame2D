using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : UIBase
{
    TMP_Text _ammoText;
    Image _weaponImage;
    GameObject _ammo; // 총알 ui 로직에 대해서는 좀더 고민해보기

    void Awake()
    {
        GameObject weaponImage = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/WeaponImage", transform);
        GameObject loadedAmmo = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/LoadedAmmo", transform);
        GameObject ammoText = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/AmmoText", transform);

        _ammoText = ammoText.GetComponent<TMP_Text>();
        _weaponImage = weaponImage.GetComponent<Image>();
        _ammo = loadedAmmo;
    }

    void Update()
    {
        if (Managers.Game.playerWeaponList == null || Managers.Game.playerWeaponList.Count == 0)
            return;

        WeaponBase weapon = Managers.Game.playerWeaponList[0];
        _ammoText.text = $"{weapon.CurAmmo + weapon.CurLoadedAmmo}/{weapon.MaxAmmo}";
    }
}
