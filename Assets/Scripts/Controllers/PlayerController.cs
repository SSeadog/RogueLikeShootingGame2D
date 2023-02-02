using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    GunBase curWeapon;

    float reloadingTimer = 0f;
    bool isReloading = false;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        GameObject ori = Resources.Load<GameObject>("Prefabs/Weapons/" + _stat.CurWeaponType.ToString());
        GameObject instance = Instantiate(ori, transform);
        curWeapon = instance.GetComponent<GunBase>();
    }

    void Update()
    {
        if (isReloading)
        {
            reloadingTimer += Time.deltaTime;

            //if (reloadingTimer > GunBase.)
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isReloading)
                return;

            if (curWeapon.GetCurLoadedAmmo() == 0)
            {
                curWeapon.Reload();
                isReloading = true;
            }
            else
            {
                curWeapon.Fire();
            }
        }
    }

    void FixedUpdate()
    {
        Move();
        RotateGun();
    }

    void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized;

        transform.Translate(moveVec * Time.deltaTime * _stat.Speed);
    }

    void RotateGun()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - curWeapon.transform.position.y, mouse.x - curWeapon.transform.position.x) * Mathf.Rad2Deg;
        curWeapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
