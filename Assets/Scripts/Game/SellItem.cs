using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    private GameObject _item;
    private int _price = 0;
    private bool _isSold = false;
    private bool _isPlayerInTrigger = false;

    public void SetItem(string path, int price = 10)
    {
        _item = Managers.Resource.Instantiate(path, transform);
        _item.GetComponent<CircleCollider2D>().enabled = false;

        _price = price;
    }

    private void Update()
    {
        if (_isSold)
            return;

        if (_isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (Managers.Game.Gold > _price)
            {
                Managers.Game.Gold -= _price;
                _isSold = true;
                _item.GetComponent<ItemBase>().Effect();

                _isPlayerInTrigger = false;
                Destroy(_item);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isSold)
            return;

        if (!collision.CompareTag("Player"))
            return;

        Debug.Log($"{_item.name} {_price} 알리는 UI제작필요!!!");
        _isPlayerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isSold)
            return;

        if (!collision.CompareTag("Player"))
            return;

        _isPlayerInTrigger = false;
    }
}
