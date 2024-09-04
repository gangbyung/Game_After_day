using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite dragSprite;
    public RectTransform[] dropZones; // ���� ��� ���� RectTransform

    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f); // ���콺 ���� �� Ȯ���� ����
    public Vector3 dragScale = new Vector3(1.5f, 1.5f, 1f); // �巡�� �� Ȯ���� ����

    private Image imageComponent;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private bool isDraggable = true; // �巡�� ���� ���¸� ��Ÿ���� ����
    private bool isScaling = true; // ���콺 ���� �� ũ�� ���� ����� ��Ÿ���� ����
    private RectTransform currentDropZone; // ���� ��� ����

    // ��� ���� ���θ� ������ �̺�Ʈ
    public delegate void DropStatusChangeHandler();
    public event DropStatusChangeHandler OnDropSuccess;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        originalPosition = transform.position;
        originalScale = transform.localScale; // ���� ������ ����
        originalRotation = transform.rotation; // ���� ȸ�� ����
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            imageComponent.sprite = dragSprite; // �巡�� ���� ��������Ʈ�� ����
            transform.localScale = dragScale; // �巡�� �� Ȯ��
            transform.rotation = Quaternion.Euler(0, 0, 0); // Z�� ȸ���� 0���� ����
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
                // ��� ������ ������Ʈ�� ��ġ�մϴ�.
                transform.position = currentDropZone.position;
                imageComponent.sprite = dragSprite; // �巡�� ���� ��������Ʈ�� ����
                transform.localScale = dragScale; // �巡�� �� ũ�� ����
                transform.rotation = Quaternion.Euler(0, 0, 0); // Z�� ȸ���� 0���� ����

                // �巡�� ��� ��Ȱ��ȭ
                isDraggable = false;
                isScaling = false; // ���콺 ���� �� ũ�� ���� ��Ȱ��ȭ

                // ��� ���� �̺�Ʈ ȣ��
                OnDropSuccess?.Invoke();
            }
            else
            {
                // ��� ���� �ۿ� ���� ��
                transform.position = originalPosition;
                imageComponent.sprite = normalSprite;
                transform.localScale = originalScale; // ���� ũ��� ����
                transform.rotation = originalRotation; // ���� ȸ������ ����
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
        if (isScaling && isDraggable) // �巡�� ���� ������ ���� ������ ����
        {
            transform.localScale = hoverScale; // ���콺 ���� �� Ȯ��
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isScaling)
        {
            transform.localScale = originalScale; // ���� ũ��� ����
        }
    }
}
