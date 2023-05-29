using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] private List<string> _voidTagList = new List<string>();
    [SerializeField] private GameObject _effect;
    private float _power;

    public float Power { get { return _power; } set { _power = value; } }

    protected virtual void OnTriggerEnterAction(Collider2D collision)
    {
        bool isCollisionIgnore = false;
        foreach (string tagName in _voidTagList)
        {
            if (collision.CompareTag(tagName) == true)
            {
                isCollisionIgnore = true;
                break;
            }
        }

        if (isCollisionIgnore == false)
        {
            GameObject effect = Instantiate(_effect, transform.position, Quaternion.identity);
            effect.GetComponent<BulletEffect>().Init(transform.GetComponent<Rigidbody2D>().velocity.normalized);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterAction(collision);
    }
}
