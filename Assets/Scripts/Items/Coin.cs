using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int value = 1;
    float _followSpeed = 4f;

    public void GetCoin(Transform target)
    {
        StartCoroutine(CoGetCoin(target));
    }

    IEnumerator CoGetCoin(Transform target)
    {
        while (Vector3.Distance(target.position, transform.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * _followSpeed);
            yield return null;
        }

        Managers.Game.Gold += value;
        Destroy(gameObject);

        Debug.Log(Managers.Game.Gold);
    }
}
