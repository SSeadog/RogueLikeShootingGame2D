using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float _speed = 3f;
    float _attackRange = 3f;

    GameObject _target;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
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

        transform.Translate(moveVec.normalized * _speed * Time.deltaTime);
    }    
}
