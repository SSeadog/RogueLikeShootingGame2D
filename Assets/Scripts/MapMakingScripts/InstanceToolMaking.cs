using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstanceToolMaking : MonoBehaviour
{
    BoxCollider2D _collider;

    public void Init(BoxCollider2D collider)
    {
        _collider = collider;
    }

    public void OnLineVerticalDrag(BaseEventData baseEvent)
    {
        PointerEventData pointerEvent = (PointerEventData)baseEvent;

        Vector2 delta = pointerEvent.delta;
        float roomScale = 100f;

        RectTransform rectTr = transform.parent.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(pointerEvent.position);
        if (pointerWorldPos.y < transform.position.y)
        {
            rect.height = rectTr.sizeDelta.y - delta.y;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + delta.y / roomScale / 2);
        }
        else
        {
            rect.height = rectTr.sizeDelta.y + delta.y;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + delta.y / roomScale / 2);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;

        if (_collider!= null)
            _collider.size = rect.size;
    }

    public void OnLineHorizontalDrag(BaseEventData baseEvent)
    {
        PointerEventData pointerEvent = (PointerEventData)baseEvent;

        Vector2 delta = pointerEvent.delta;
        float roomScale = 100f;

        RectTransform rectTr = transform.parent.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(pointerEvent.position);
        if (pointerWorldPos.x < transform.position.x)
        {
            rect.width = rectTr.sizeDelta.x - delta.x;
            rect.position = new Vector2(rectTr.position.x + delta.x / roomScale / 2, rectTr.position.y);
        }
        else
        {
            rect.width = rectTr.sizeDelta.x + delta.x;
            rect.position = new Vector2(rectTr.position.x + delta.x / roomScale / 2, rectTr.position.y);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;

        if (_collider != null)
            _collider.size = rect.size;
    }

    public void OnDrag(BaseEventData baseEvent)
    {
        if (Input.GetMouseButton(2))
            return;

        PointerEventData pointerEvent = (PointerEventData)baseEvent;
        transform.parent.Translate(pointerEvent.delta * 0.1f);
    }
}
