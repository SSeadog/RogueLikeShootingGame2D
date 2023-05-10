using UnityEngine;
using UnityEngine.EventSystems;

public class InstanceToolMaking : MonoBehaviour
{
    // 생성될 때 크기 가져와서 보더 크기 적절히 생성
    // 끌고 움직일 때도 screenPoint로 옮기기
    // 확대, 축소는..?
    [SerializeField] RectTransform lineTop;
    [SerializeField] RectTransform lineBottom;
    [SerializeField] RectTransform lineLeft;
    [SerializeField] RectTransform lineRight;
    BoxCollider2D _collider;
    Vector3 _beforeMousePos;
    float _roomScale = 100f;

    public void Init(BoxCollider2D collider)
    {
        _collider = collider;

        {
            Rect rect = lineTop.rect;
            rect.height = rect.height * Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.ZoomLevel / 9f;
            lineTop.sizeDelta = new Vector2(0f, rect.height);
        }
        {
            Rect rect = lineBottom.rect;
            rect.height = rect.height * Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.ZoomLevel / 9f;
            lineBottom.sizeDelta = new Vector2(0f, rect.height);
        }
        {
            Rect rect = lineLeft.rect;
            rect.width = rect.width * Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.ZoomLevel / 9f;
            lineLeft.sizeDelta = new Vector2(rect.width, 0f);
        }
        {
            Rect rect = lineRight.rect;
            rect.width = rect.width * Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.ZoomLevel / 9f;
            lineRight.sizeDelta = new Vector2(rect.width, 0f);
        }
    }

    public void OnLineVerticalDrag(BaseEventData baseEvent)
    {
        if (_beforeMousePos == Vector3.zero)
            return;

        Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveVec = curMousePos - _beforeMousePos;
        Vector2 delta = new Vector2(moveVec.x, moveVec.y);

        RectTransform rectTr = transform.parent.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pointerWorldPos.y < transform.position.y)
        {
            rect.height = rectTr.sizeDelta.y - delta.y * _roomScale;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + delta.y / 2);
        }
        else
        {
            rect.height = rectTr.sizeDelta.y + delta.y * _roomScale;
            rect.position = new Vector2(rectTr.position.x, rectTr.position.y + delta.y / 2);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;

        _beforeMousePos = curMousePos;

        if (_collider!= null)
            _collider.size = rect.size;
    }

    public void OnLineHorizontalDrag(BaseEventData baseEvent)
    {
        if (_beforeMousePos == Vector3.zero)
            return;

        Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveVec = curMousePos - _beforeMousePos;
        Vector2 delta = new Vector2(moveVec.x, moveVec.y);

        RectTransform rectTr = transform.parent.GetComponent<RectTransform>();
        Rect rect = new Rect(rectTr.position.x, rectTr.position.y, rectTr.sizeDelta.x, rectTr.sizeDelta.y);

        Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pointerWorldPos.x < transform.position.x)
        {
            rect.width = rectTr.sizeDelta.x - delta.x * _roomScale;
            rect.position = new Vector2(rectTr.position.x + delta.x / 2, rectTr.position.y);
        }
        else
        {
            rect.width = rectTr.sizeDelta.x + delta.x * _roomScale;
            rect.position = new Vector2(rectTr.position.x + delta.x / 2, rectTr.position.y);
        }

        rectTr.position = rect.position;
        rectTr.sizeDelta = rect.size;

        _beforeMousePos = curMousePos;

        if (_collider != null)
            _collider.size = rect.size;
    }

    public void OnPointerDown(BaseEventData baseEvent)
    {
        _beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(BaseEventData baseEvent)
    {
        if (Input.GetMouseButton(2))
            return;

        if (_beforeMousePos == Vector3.zero)
            return;

        Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveVec = curMousePos - _beforeMousePos;

        transform.parent.Translate(moveVec);
        _beforeMousePos = curMousePos;
    }

    public void OnPointerUp(BaseEventData baseEvent)
    {
        _beforeMousePos = Vector3.zero;
    }
}
