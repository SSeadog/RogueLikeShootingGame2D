using UnityEngine;
using UnityEngine.Events;

public class Stat : MonoBehaviour
{
    [SerializeField] protected Define.ObjectType type;

    public UnityAction onGetDamagedAction;
    public UnityAction onDeadAction;

    protected float _maxHp;
    protected float _hp;
    protected float _speed;

    public float MaxHp { get { return _maxHp; } }
    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }

    public virtual void Init()
    {
    }

    public void GetDamaged(float damage)
    {
        _hp -= damage;
        if (_hp > 0)
        {
            onGetDamagedAction?.Invoke();
        }
        else
        {
            _hp = 0;
            Die();
        }
    }

    void Die()
    {
        onDeadAction?.Invoke();
    }
}
