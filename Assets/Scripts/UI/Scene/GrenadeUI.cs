using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeUI : UIBase
{
    [SerializeField] GameObject _grenadeIcon;
    int _curCount = 0;
    List<GameObject> _icons = new List<GameObject>();

    void Start()
    {
        SetGrenadeCount(Managers.Game.grenade);
    }

    void Update()
    {
        SetGrenadeCount(Managers.Game.grenade);
    }

    public void SetGrenadeCount(int count)
    {
        // ������ �߰�
        if (count > _curCount)
        {
            int addCount = count - _curCount;
            for (int i = 0; i < addCount; i++)
            {
                GameObject instance = Instantiate(_grenadeIcon, transform);
                _icons.Add(instance);
            }
        }
        // ������ ����
        else if (count < _curCount)
        {
            int removeCount = _curCount - count;
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(_icons[0]);
                _icons.RemoveAt(0);
            }
        }
        _curCount = count;
    }
}
