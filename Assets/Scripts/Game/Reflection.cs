using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���÷��� Ʈ���� ����");
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("���÷��� Ʈ���� ����");
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
