using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    float _maxSize = 7f;
    float _curSize = 3f;
    public void Explode()
    {
        StartCoroutine(CoExplode());
    }

    IEnumerator CoExplode()
    {
        while (_curSize < _maxSize)
        {
            _curSize += 0.1f;
            transform.localScale = Vector3.one * _curSize;
            yield return null;
        }

        Destroy(gameObject);
    }
}
