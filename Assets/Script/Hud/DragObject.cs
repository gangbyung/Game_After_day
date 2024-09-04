using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sprite normalSprite;
    public Sprite dragSprite;

    public RectTransform dropZoneRectTransform; // 드롭 구역 RectTransform

    private Image imageComponent;
    private Vector3 originalPosition;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        imageComponent.sprite = dragSprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsInDropZone(eventData.position))
        {
            // 드롭 구역에 오브젝트를 배치합니다.
            transform.position = dropZoneRectTransform.position;
            imageComponent.sprite = normalSprite;
        }
        else
        {
            // 드롭 구역 밖에 있을 때
            transform.position = originalPosition;
            imageComponent.sprite = normalSprite;
        }
    }

    private bool IsInDropZone(Vector3 position)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dropZoneRectTransform, position, null, out localPoint);
        return dropZoneRectTransform.rect.Contains(localPoint);
    }
}
