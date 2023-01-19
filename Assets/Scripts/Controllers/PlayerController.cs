using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;

    void Start()
    {
        Stat playerStat = GetComponent<Stat>();

        _speed = playerStat.Speed;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Press Q And Call Manager.Instance");
            Manager mg = Manager.Instance;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized;

        transform.Translate(moveVec * Time.deltaTime * _speed);
    }
}
