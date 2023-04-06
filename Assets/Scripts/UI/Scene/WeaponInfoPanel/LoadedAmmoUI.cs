using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadedAmmoUI : UIBase
{
    // �ִ� �ε� ������ bullet��ŭ �̹����� �����ؼ� �����ϱ�
    // �Ѿ��� �򶧸��� ���������� �ϳ��� �̹��� ���ֱ�
    // �������ϸ� �ε�� �Ѿ˼���ŭ �Ʒ��������� �̹��� ���̰� �ϱ�
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
