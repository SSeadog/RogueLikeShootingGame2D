using System.Collections;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    private float _followSpeed = 4f;
    private float _speedUpTimer = 3f;
    private bool _isSpeedUp = false;

    public abstract void Effect(Stat stat);

    public void GetItem(Transform target)
    {
        StartCoroutine(CoGetItem(target));
    }

    IEnumerator CoGetItem(Transform target)
    {
        while (Vector3.Distance(target.position, transform.position) > 0.5f)
        {
            _speedUpTimer -= Time.deltaTime;
            if (_isSpeedUp == false && _speedUpTimer < 0)
            {
                _isSpeedUp = true;
                _followSpeed = _followSpeed * 2f;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * _followSpeed);
            yield return null;
        }

        Stat stat = target.GetComponent<Stat>();
        if (stat != null)
            Effect(stat);
        else
            Effect(null);
    }
}
