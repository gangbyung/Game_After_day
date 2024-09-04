using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject targetGameObject; // 비활성화할 게임 오브젝트
    public DragObject[] draggableObjects; // 드래그 가능한 이미지 배열

    private int totalDraggableObjects; // 전체 드래그 가능한 오브젝트 수
    private int successfulDrops; // 성공적으로 드롭된 이미지 수

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
            // 모든 이미지가 드롭된 경우
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // 게임 오브젝트 비활성화
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
