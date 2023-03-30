using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImagePanel : UIBase
{
    [SerializeField] TMP_Text _ammoText;
    [SerializeField] Image _weaponImage;
    [SerializeField] GameObject _ammo; // 총알 ui 로직에 대해서는 좀더 고민해보기

    void Update()
    {
        WeaponBase weapon = Managers.Game.playerWeaponList[0];
        _ammoText.text = $"{weapon.CurAmmo + weapon.CurLoadedAmmo}/{weapon.MaxAmmo}";
    }
}
