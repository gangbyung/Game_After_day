using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite dragSprite;
    public RectTransform[] dropZones; // 여러 드롭 구역 RectTransform

    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f); // 마우스 오버 시 확대할 비율
    public Vector3 dragScale = new Vector3(1.5f, 1.5f, 1f); // 드래그 중 확대할 비율

    private Image imageComponent;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private bool isDraggable = true; // 드래그 가능 상태를 나타내는 변수
    private bool isScaling = true; // 마우스 오버 시 크기 변경 기능을 나타내는 변수
    private RectTransform currentDropZone; // 현재 드롭 구역

    // 드롭 성공 여부를 관리할 이벤트
    public delegate void DropStatusChangeHandler();
    public event DropStatusChangeHandler OnDropSuccess;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        originalPosition = transform.position;
        originalScale = transform.localScale; // 원래 스케일 저장
        originalRotation = transform.rotation; // 원래 회전 저장
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            imageComponent.sprite = dragSprite; // 드래그 상태 스프라이트로 변경
            transform.localScale = dragScale; // 드래그 중 확대
            transform.rotation = Quaternion.Euler(0, 0, 0); // Z축 회전을 0으로 설정
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            bool isInAnyDropZone = false;

            foreach (var dropZone in dropZones)
            {
                if (IsInDropZone(eventData.position, dropZone))
                {
                    currentDropZone = dropZone;
                    isInAnyDropZone = true;
                    break;
                }
            }

            if (isInAnyDropZone)
            {
                // 드롭 구역에 오브젝트를 배치합니다.
                transform.position = currentDropZone.position;
                imageComponent.sprite = dragSprite; // 드래그 상태 스프라이트로 유지
                transform.localScale = dragScale; // 드래그 중 크기 유지
                transform.rotation = Quaternion.Euler(0, 0, 0); // Z축 회전을 0으로 유지

                // 드래그 기능 비활성화
                isDraggable = false;
                isScaling = false; // 마우스 오버 시 크기 변경 비활성화

                // 드롭 성공 이벤트 호출
                OnDropSuccess?.Invoke();
            }
            else
            {
                // 드롭 구역 밖에 있을 때
                transform.position = originalPosition;
                imageComponent.sprite = normalSprite;
                transform.localScale = originalScale; // 원래 크기로 복원
                transform.rotation = originalRotation; // 원래 회전으로 복원
            }
        }
    }

    private bool IsInDropZone(Vector3 position, RectTransform dropZone)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dropZone, position, null, out localPoint);
        return dropZone.rect.Contains(localPoint);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isScaling && isDraggable) // 드래그 가능 상태일 때만 스케일 변경
        {
            transform.localScale = hoverScale; // 마우스 오버 시 확대
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isScaling)
        {
            transform.localScale = originalScale; // 원래 크기로 복원
        }
    }
}
