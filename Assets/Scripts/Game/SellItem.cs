using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem : MonoBehaviour
{
    private GameObject _item;
    private GameObject _sellItemUI;
    private int _price = 0;
    private bool _isSold = false;
    private bool _isPlayerInTrigger = false;

    public void SetItem(string path, int price = 10)
    {
        _item = Managers.Resource.Instantiate(path, transform);
        _item.GetComponent<CircleCollider2D>().enabled = false;
        _price = price;

        _sellItemUI = Managers.Resource.Instantiate("Prefabs/UI/World/SellItemUI", transform);
        _sellItemUI.GetComponent<SellItemUI>().SetText(_item.name, _price);
        _sellItemUI.transform.position += Vector3.up;
        _sellItemUI.SetActive(false);
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

        _isPlayerInTrigger = true;
        _sellItemUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isSold)
            return;

        if (!collision.CompareTag("Player"))
            return;

        _isPlayerInTrigger = false;
        _sellItemUI.SetActive(false);
    }
}
