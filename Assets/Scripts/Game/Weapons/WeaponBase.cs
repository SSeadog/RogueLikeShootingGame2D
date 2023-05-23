using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private Define.WeaponType weaponType = Define.WeaponType.None;
    [SerializeField] protected Transform _firePos;
    [SerializeField] protected Transform _leftHandPoint;
    [SerializeField] protected Transform _rightHandPoint;

    protected GameObject _bulletRoot;
    protected GameObject _bulletOrigin;

    protected float _power;
    protected int _maxAmmo;
    protected int _fullLoadAmmo;
    protected int _curAmmo;
    protected int _curLoadAmmo;
    protected float _bulletSpeed;
    protected float _reloadSpeed;
    protected float _reboundDelta = 0.15f;

    private SpriteRenderer _gunSprite;
    private Vector3 _initFirePos;
    private Vector3 _initLeftHandPos;
    private Vector3 _initRightHandPos;
    private float _fireSpeed;
    private bool _canFire;
    private bool _isFlipped;
    private float _curRebound;
    private float _maxRebound = 0.3f;

    public float Power { get { return _power; } }
    public int FullLoadAmmo { get { return _fullLoadAmmo; } }
    public int CurLoadAmmo { get { return _curLoadAmmo; } }
    public int MaxAmmo { get { return _maxAmmo; } }
    public int CurAmmo { get { return _curAmmo; } set { if (value < _maxAmmo) _curAmmo = value; else _curAmmo = _maxAmmo; } }
    public float ReloadSpeed { get { return _reloadSpeed; } }
    public Transform LeftHandPoint { get { return _leftHandPoint; } }
    public Transform RightHandPoint { get { return _rightHandPoint; } }

    void Awake()
    {
        Init();
    }

    void Update()
    {
        ReduceRebound();
    }

    public abstract void LoadBulletResource();
    public abstract void FireBullets();

    protected float GetCurRebound()
    {
        return Random.Range(-_curRebound, _curRebound);
    }

    public void Fire()
    {
        if (_curLoadAmmo == 0)
            return;

        if (!_canFire)
            return;

        _canFire = false;
        _curLoadAmmo--;
        Managers.Ui.GetUI<WeaponInfoPanel>().RemoveBullet();

        FireBullets();
        AddRebound();
        StartCoroutine(CoWaitFire());
    }

    void ReduceRebound()
    {
        if (_curRebound > 0f)
        {
            _curRebound -= Time.deltaTime;
        }
    }

    void AddRebound()
    {
        if (_curRebound < _maxRebound)
            _curRebound += _reboundDelta;
    }

    IEnumerator CoWaitFire()
    {
        yield return new WaitForSeconds(_fireSpeed);
        _canFire = true;
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
        {
            Vector3 newFirePos = new Vector3(_initFirePos.x, _initFirePos.y * -1f, _initFirePos.z);
            _firePos.localPosition = newFirePos;
            Vector3 newLeftHandPos = new Vector3(_initLeftHandPos.x, _initLeftHandPos.y * -1f, _initLeftHandPos.z);
            _leftHandPoint.localPosition = newLeftHandPos;
            Vector3 newRightHandPos = new Vector3(_initRightHandPos.x, _initRightHandPos.y * -1f, _initRightHandPos.z);
            _rightHandPoint.localPosition = newRightHandPos;
        }
        else
        {
            _firePos.localPosition = _initFirePos;
            _leftHandPoint.localPosition = _initLeftHandPos;
            _rightHandPoint.localPosition = _initRightHandPos;
        }
    }

    public void SetVisible(bool visible)
    {
        _gunSprite.enabled = visible;
    }

    public virtual void Reload()
    {
        if (_curLoadAmmo == _fullLoadAmmo)
            return;

        int maxReloadAmmoCount = _fullLoadAmmo - _curLoadAmmo;
        int reloadAmmoCount = Mathf.Min(_curAmmo, maxReloadAmmoCount);

        _curAmmo -= reloadAmmoCount;
        _curLoadAmmo += reloadAmmoCount;
        Managers.Ui.GetUI<WeaponInfoPanel>().FillBullet(reloadAmmoCount);
    }

    public virtual void Init()
    {
        Data.Weapon weaponInfo = Managers.Data.WeaponDict[weaponType.ToString()];

        _power = weaponInfo.power;
        _maxAmmo = weaponInfo.maxAmmo;
        _fullLoadAmmo = weaponInfo.fullLoadAmmo;
        _curAmmo = _maxAmmo - _fullLoadAmmo;
        _curLoadAmmo = _fullLoadAmmo;
        _bulletSpeed = weaponInfo.bulletSpeed;
        _fireSpeed = weaponInfo.fireSpeed;
        _reloadSpeed = weaponInfo.reloadSpeed;

        _bulletRoot = GetBulletRoot();
        _initFirePos = _firePos.localPosition;
        _initLeftHandPos = _leftHandPoint.localPosition;
        _initRightHandPos = _rightHandPoint.localPosition;

        _gunSprite = GetComponentInChildren<SpriteRenderer>();

        LoadBulletResource();
        _canFire = true;
    }

    public void Swap()
    {
        _canFire = true;
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

    protected float GetMouseRotRad()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = (worldMousePoint - _firePos.position).normalized;

        return Mathf.Atan2(dir.y, dir.x);
    }
}
