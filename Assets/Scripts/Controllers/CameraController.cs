using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject _player;
    float _speed = 5f;
    float _weight = 1f;

    float screenMidX;
    float screenMidY;

    float minVecMag = 50f;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        screenMidX = Screen.width / 2;
        screenMidY = Screen.height / 2;
    }

    void FixedUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 playerPos = _player.transform.position;
        Vector2 mousePos = Input.mousePosition;
        mousePos.x -= screenMidX;
        mousePos.y -= screenMidY;

        Vector3 mousePos3;
        if (mousePos.magnitude > minVecMag)
            mousePos3 = mousePos.normalized;
        else
            mousePos3 = Vector3.zero;

        Vector3 moveVec = playerPos + mousePos3 * _weight;
        moveVec.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, moveVec, Time.deltaTime * _speed);
    }
}
