using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // �� �̸��� �׿� ���� �� �̸� ����
        loadingUIManager.SetMapNameForScene("1.part0", "������ �αٽ�");
        loadingUIManager.SetMapNameForScene("2.part1", "����");
    }
}