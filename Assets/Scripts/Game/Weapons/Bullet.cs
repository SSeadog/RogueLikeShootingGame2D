using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private List<string> _voidTagList = new List<string>();
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnterAction(collision);
    }
}
