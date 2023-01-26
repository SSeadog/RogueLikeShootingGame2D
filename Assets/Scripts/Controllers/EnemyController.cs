using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Stat _stat;
    float _attackRange = 3f;

    GameObject _target;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _stat = GetComponent<Stat>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveVec = _target.transform.position - transform.position;
        if (moveVec.magnitude < _attackRange)
            return;

        transform.Translate(moveVec.normalized * _stat.Speed * Time.deltaTime);
    }    
}
