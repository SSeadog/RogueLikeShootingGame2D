using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public Define.WeaponType weaponType = Define.WeaponType.None;

    protected GameObject _bulletRoot;
    protected GameObject _bulletOrigin;
    protected Transform _firePos;

    protected float _power;
    protected int _maxAmmo;
    protected int _fullLoadAmmo;

    [SerializeField] protected int _curAmmo;
    [SerializeField] protected int _curLoadAmmo;

    Vector3 _initFirePos;

    float reloadingTime;

    bool _isFlipped;
    SpriteRenderer _gunSprite;



    public int FullLoadAmmo { get { return _fullLoadAmmo; } }
    public int CurLoadAmmo { get { return _curLoadAmmo; } }
    public int MaxAmmo { get { return _maxAmmo; } }
    public int CurAmmo { get { return _curAmmo; } }


    void Awake()
    {
        Init();
    }

    public abstract void LoadBulletResource();
    public abstract void GenerateBullets();

    public void Fire()
    {
        if (_curLoadAmmo == 0)
            return;

        _curLoadAmmo--;
        GenerateBullets();
        Managers.Ui.GetUI<LoadedAmmoUI>().RemoveBullet();
    }

    public float GetReloadingTime()
    {
        return 1f;
    }

    public void Flip(bool isFlip)
    {
        if (_gunSprite == null)
            return;

        if (_isFlipped == isFlip)
            return;

        _isFlipped = isFlip;
        _gunSprite.flipY = _isFlipped;

        if (_isFlipped)
            _firePos.localPosition = _initFirePos + Vector3.down * 0.35f;
        else
            _firePos.localPosition = _initFirePos;
    }

    public virtual void Reload()
    {
        if (_curLoadAmmo == _fullLoadAmmo)
            return;

        int maxReloadAmmoCount = _fullLoadAmmo - _curLoadAmmo;
        int reloadAmmoCount = Mathf.Min(_curAmmo, maxReloadAmmoCount);

        _curAmmo -= reloadAmmoCount;
        _curLoadAmmo += reloadAmmoCount;
        Managers.Ui.GetUI<LoadedAmmoUI>().FillBullet(reloadAmmoCount);
    }

    public virtual void Init()
    {
        Data.Weapon weaponInfo = Managers.Data.weaponDict[weaponType.ToString()];

        _power = weaponInfo.power;
        _maxAmmo = weaponInfo.maxAmmo;
        _fullLoadAmmo = weaponInfo.fullLoadAmmo;
        _curAmmo = _maxAmmo - _fullLoadAmmo;
        _curLoadAmmo = _fullLoadAmmo;

        _bulletRoot = GetBulletRoot();
        _firePos = transform.Find("FirePos");
        _initFirePos = _firePos.localPosition;

        _gunSprite = GetComponentInChildren<SpriteRenderer>();

        LoadBulletResource();
    }

    GameObject GetBulletRoot()
    {
        GameObject _bulletRoot = GameObject.Find("BulletControll");
        if (_bulletRoot == null)
        {
            _bulletRoot = new GameObject("BulletControll");
        }

        return _bulletRoot;
    }
}
