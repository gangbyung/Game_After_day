using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject targetGameObject; // ��Ȱ��ȭ�� ���� ������Ʈ
    public DragObject[] draggableObjects; // �巡�� ������ �̹��� �迭

    private int totalDraggableObjects; // ��ü �巡�� ������ ������Ʈ ��
    private int successfulDrops; // ���������� ��ӵ� �̹��� ��

    private void Start()
    {
        totalDraggableObjects = draggableObjects.Length;

        foreach (var draggableObject in draggableObjects)
        {
            draggableObject.OnDropSuccess += OnObjectDropped;
        }
    }

    private void OnObjectDropped()
    {
        successfulDrops++;

        if (successfulDrops >= totalDraggableObjects)
        {
            // ��� �̹����� ��ӵ� ���
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var draggableObject in draggableObjects)
        {
            draggableObject.OnDropSuccess -= OnObjectDropped;
        }
    }
}
