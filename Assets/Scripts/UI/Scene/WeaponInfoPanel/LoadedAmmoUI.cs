using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadedAmmoUI : UIBase
{
    // 최대 로드 가능한 bullet만큼 이미지를 생성해서 관리하기
    // 총알을 쏠때마다 위에서부터 하나씩 이미지 없애기
    // 재장전하면 로드된 총알수만큼 아래에서부터 이미지 보이게 하기
    List<Image> _bulletIconList = new List<Image>();
    int _maxIndex = 0;
    int _currentIndex = 0;

    protected override void Init()
    {
        base.Init();
        WeaponBase weapon = Managers.Game.playerWeaponList[0];

        for (int i = 0; i < weapon.FullLoadAmmo; i++)
        {
            GameObject instance = Managers.Resource.LoadUI("Prefabs/UI/SubItem/AmmoUI", transform);
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
}
