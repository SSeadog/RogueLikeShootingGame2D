using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("리플렉션 트리거 엔터");
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("리플렉션 트리거 엑싯");
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
