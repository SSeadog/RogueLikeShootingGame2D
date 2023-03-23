using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItem
{
    public SellItem(Define.ItemType type, int price)
    {
        this.type = type;
        this.price = price;
    }

    public Define.ItemType type;
    public int price;
    public bool isSoldOut;
}

public class Store : MonoBehaviour
{
    // 판매할 아이템을 랜덤으로 생성하여 가지고 있기
    // 아이템의 가격, 판매여부도 관리해야함

    public SellItem[] items = new SellItem[3];

    private float _itemOffsetX = -3f;
    private float _itemPaddingX = 3f;
    private float _itemOffsetY = -3f;

    void Start()
    {
        // 임시로 할당. 랜덤 생성하도록 변경 필요
        items[0] = new SellItem(Define.ItemType.Food, 1);
        items[1] = new SellItem(Define.ItemType.Ammo, 1);
        items[2] = new SellItem(Define.ItemType.Key, 1);

        for (int i = 0; i < items.Length; i++)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/Items/" + items[i].type.ToString(), transform);
            instance.transform.position = transform.position + new Vector3(_itemOffsetX + _itemPaddingX * i, _itemOffsetY);
        }
    }

    void Update()
    {
        
    }
}
