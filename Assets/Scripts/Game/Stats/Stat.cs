using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour
{
    [SerializeField] protected Define.ObjectType type;

    public UnityAction onGetDamagedAction;
    public UnityAction onRecoveryAction;
    public UnityAction onDeadAction;

    protected float _maxHp;
    protected float _hp;
    protected float _speed;
    private bool _isDead = false;

    public float MaxHp { get { return _maxHp; } }
    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }

    public virtual void Init()
    {
    }

    public void RecoverHp(float increaseHp)
    {
        if (_hp + increaseHp < _maxHp)
            _hp += increaseHp;
        else
            _hp = _maxHp;

        onRecoveryAction?.Invoke();
    }

    public void GetDamaged(float damage)
    {
        _hp -= damage;
        onGetDamagedAction?.Invoke();

        if (_hp <= 0)
        {
            _hp = 0;
            Die();
        }
    }

    void Die()
    {
        if (_isDead)
            return;

        onDeadAction?.Invoke();
        _isDead = false;
    }
}
