using UnityEngine;

public class Store : MonoBehaviour
{
    // 판매할 아이템을 랜덤으로 생성하여 가지고 있기
    private SellItem[] _sellItems = new SellItem[3];
    private int _itemCount = 3;
    private float _itemOffsetX = -3f;
    private float _itemPaddingX = 3f;
    private float _itemOffsetY = -3f;

    void Start()
    {
        GenerateRandomItems();
    }

    void GenerateRandomItems()
    {
        Define.ItemType[] itemTypes = new Define.ItemType[3] { Define.ItemType.Ammo, Define.ItemType.Food, Define.ItemType.Key };

        for (int i = 0; i < _itemCount; i++)
        {
            GameObject instance = Managers.Resource.Instantiate("Prefabs/SellItem", transform);
            instance.transform.position = transform.position + new Vector3(_itemOffsetX + _itemPaddingX * i, _itemOffsetY);
            instance.GetComponent<SellItem>().SetItem("Prefabs/Items/" + itemTypes[i].ToString());
            _sellItems[i] = instance.GetComponent<SellItem>();
        }
    }

    void Update()
    {
        
    }
}
