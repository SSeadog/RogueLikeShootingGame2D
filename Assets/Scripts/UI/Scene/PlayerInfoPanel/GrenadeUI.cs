using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeUI : UIBase
{
    [SerializeField] private GameObject _grenadeIcon;
    private List<GameObject> _icons = new List<GameObject>();
    private int _curCount = 0;

    void Start()
    {
        SetGrenadeCount(Managers.Game.Grenade);
    }

    void Update()
    {
        SetGrenadeCount(Managers.Game.Grenade);
    }

    public void SetGrenadeCount(int count)
    {
        // 많으면 추가
        if (count > _curCount)
        {
            int addCount = count - _curCount;
            for (int i = 0; i < addCount; i++)
            {
                GameObject instance = Instantiate(_grenadeIcon, transform);
                _icons.Add(instance);
            }
        }
        // 적으면 삭제
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
