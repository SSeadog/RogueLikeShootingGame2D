using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    protected GameObject _bulletRoot;
    protected GameObject _bulletOrigin;
    protected Transform _firePos;

    protected float _firePower;

    protected int _maxAmmo;
    protected int _curAmmo;
    protected int _loadedBullet;

    void Start()
    {
        Init();
    }

    public abstract void LoadBulletResource();
    public abstract void Fire();
    public abstract void Reload();

    public virtual void Init()
    {
        _bulletRoot = GameObject.Find("BulletControll");
        if (_bulletRoot == null)
        {
            _bulletRoot = new GameObject("BulletControll");
        }

        LoadBulletResource();

        _firePos = transform.Find("FirePos");

        _firePower = 1f;

        _maxAmmo = 600;
        _curAmmo = 120;
        _loadedBullet = 30;

    }
}
