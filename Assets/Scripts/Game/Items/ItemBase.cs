using System.Collections;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    private float _followSpeed = 4f;

    public abstract void Effect();

    public void GetItem(Transform target)
    {
        StartCoroutine(CoGetItem(target));
    }

    IEnumerator CoGetItem(Transform target)
    {
        while (Vector3.Distance(target.position, transform.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * _followSpeed);
            yield return null;
        }

        Effect();
    }
}
