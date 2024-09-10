using UnityEngine;

public class DropManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static DropManager Instance { get; private set; }

    public GameObject targetGameObject; // ��Ȱ��ȭ�� ���� ������Ʈ
    public DragObject[] draggableObjects; // �巡�� ������ �̹��� �迭

    private int totalDraggableObjects; // ��ü �巡�� ������ ������Ʈ ��
    private int successfulDrops; // ���������� ��ӵ� �̹��� ��

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ڽ��� �ı��մϴ�.
            return;
        }

        // �� ������Ʈ�� �� ��ȯ �� �ı����� �ʵ��� �����մϴ�.
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
            // ��� �̹����� ��ӵ� ���
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
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

        // �ν��Ͻ��� ���� �ν��Ͻ���� null�� ����
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
