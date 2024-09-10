using UnityEngine;

public class DropManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static DropManager Instance { get; private set; }

    public GameObject targetGameObject; // 비활성화할 게임 오브젝트
    public DragObject[] draggableObjects; // 드래그 가능한 이미지 배열

    private int totalDraggableObjects; // 전체 드래그 가능한 오브젝트 수
    private int successfulDrops; // 성공적으로 드롭된 이미지 수

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴합니다.
            return;
        }

        // 이 오브젝트가 씬 전환 시 파괴되지 않도록 설정합니다.
        DontDestroyOnLoad(gameObject);
    }

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
                GameManager.Instance.TalkBugpix();

                NpcAction.Instance.NpcUnLock16();
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var draggableObject in draggableObjects)
        {
            draggableObject.OnDropSuccess -= OnObjectDropped;
        }

        // 인스턴스가 현재 인스턴스라면 null로 설정
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
