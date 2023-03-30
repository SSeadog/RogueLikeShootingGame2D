using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoPanel : MonoBehaviour
{
    void Awake()
    {
        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel/GoldUI", transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel/GrenadeUI", transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel/HpBarUI", transform);
        Managers.Resource.LoadUI("Prefabs/UI/Scene/PlayerInfoPanel/KeyUI", transform);
    }
}
