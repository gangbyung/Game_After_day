using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // �� �̸��� �׿� ���� �� �̸� ����
        loadingUIManager.SetMapNameForScene("1.part0", "�������");
        loadingUIManager.SetMapNameForScene("2.part1", "����Ʈ����");
    }
}