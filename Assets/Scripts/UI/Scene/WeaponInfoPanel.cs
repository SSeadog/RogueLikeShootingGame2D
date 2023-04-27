using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : UIBase
{
    private TMP_Text _ammoText;
    private Image _weaponImage;
    private LoadedAmmoUI _ammo; // �Ѿ� ui ������ ���ؼ��� ���� ����غ���

    void Awake()
    {
        GameObject weaponImage = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/WeaponImage", transform);
        GameObject loadedAmmo = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/LoadedAmmoUI", transform);
        GameObject ammoText = Managers.Resource.LoadUI("Prefabs/UI/Scene/WeaponInfoPanel/AmmoText", transform);

        _ammoText = ammoText.GetComponent<TMP_Text>();
        _weaponImage = weaponImage.GetComponent<Image>();
        _ammo = loadedAmmo.GetComponent<LoadedAmmoUI>();
    }

    void Update()
    {
        if (Managers.Game.PlayerWeaponList == null || Managers.Game.PlayerWeaponList.Count == 0)
            return;

        WeaponBase weapon = Managers.Game.PlayerWeaponList[0];
        _ammoText.text = $"{weapon.CurLoadAmmo}/{weapon.CurAmmo}";
    }
}
