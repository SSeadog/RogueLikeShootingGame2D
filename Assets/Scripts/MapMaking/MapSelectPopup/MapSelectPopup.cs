using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectPopup : UIBase
{
    public void ShowPopup() {
        gameObject.SetActive(true);
    }

    public void HidePopup() {
        gameObject.SetActive(false);
    }
}
