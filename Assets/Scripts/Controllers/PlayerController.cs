using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;

    GameObject curWeapon;

    void Start()
    {
        _stat = GetComponent<PlayerStat>();

        GameObject ori = Resources.Load<GameObject>("Prefabs/Weapons/" + _stat.CurWeaponType.ToString());
        curWeapon = Instantiate(ori, transform);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            curWeapon.GetComponent<GunBase>().Fire();
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
