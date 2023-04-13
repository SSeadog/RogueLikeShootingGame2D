using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomTool : MonoBehaviour, IDragHandler
{
    BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void OnLineVerticalDrag(BaseEventData baseEvent)
    {
        PointerEventData pointerEvent = (PointerEventData)baseEvent;
        Debug.Log(pointerEvent.delta);

        RectTransform rectTr = gameObject.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(pointerEvent.position);
        if (pointerWorldPos.y < transform.position.y)
        {
            rect.height = rectTr.sizeDelta.y - pointerEvent.delta.y / 50;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + pointerEvent.delta.y / 50 / 2);
        }
        else
        {
            rect.height = rectTr.sizeDelta.y + pointerEvent.delta.y / 50;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + pointerEvent.delta.y / 50 / 2);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;
        _collider.size = rect.size;
    }

    public void OnLineHorizontalDrag(BaseEventData baseEvent)
    {
        PointerEventData pointerEvent = (PointerEventData)baseEvent;
        Debug.Log(pointerEvent.delta);

        RectTransform rectTr = gameObject.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(pointerEvent.position);
        if (pointerWorldPos.x < transform.position.x)
        {
            rect.width = rectTr.sizeDelta.x - pointerEvent.delta.x / 50;
            rect.position = new Vector2(rectTr.position.x + pointerEvent.delta.x / 50 / 2, rectTr.position.y);
        }
        else
        {
            rect.width = rectTr.sizeDelta.x + pointerEvent.delta.x / 50;
            rect.position = new Vector2(rectTr.position.x + pointerEvent.delta.x / 50 / 2, rectTr.position.y);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;
        _collider.size = rect.size;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.delta);
    }
}
