using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    // �������� ����, �Ǹſ��� �����ؾ���

    GameObject _item;
    int _price = 0;
    bool _isSold = false;

    bool _isPlayerInTrigger = false;

    public void SetItem(string path, int price = 10)
    {
        _item = Managers.Resource.Instantiate(path, transform);
        _item.GetComponent<CircleCollider2D>().enabled = false;

        _price = price;
    }

    private void Update()
    {
        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"{_item.name} ���� �õ�!!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Debug.Log($"{_item.name} {_price}!!!");
        _isPlayerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        _isPlayerInTrigger = false;
    }
}
