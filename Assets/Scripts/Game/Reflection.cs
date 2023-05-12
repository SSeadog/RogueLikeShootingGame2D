using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _reflection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            Debug.Log("리플렉션 트리거 엔터");
            _reflection.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            Debug.Log("리플렉션 트리거 엑싯");
            _reflection.enabled = false;
        }
    }
}
