using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    List<string> _voidTagList = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
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
}
