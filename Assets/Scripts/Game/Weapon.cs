using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    Define.WeaponType curWeaponType;
    GameObject curWeapon;

    void Start()
    {
        curWeaponType = Define.WeaponType.TestGun;

        GameObject ori = Resources.Load<GameObject>("Prefabs/Weapons/TestGun");
        curWeapon = Instantiate(ori, transform);
    }

    void Update()
    {
        RotateGun();
    }

    void RotateGun()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - curWeapon.transform.position.y, mouse.x - curWeapon.transform.position.x) * Mathf.Rad2Deg;
        curWeapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
