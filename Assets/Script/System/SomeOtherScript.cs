using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public LoadingUIManager loadingUIManager;

    private void Start()
    {
        // �� �̸��� �׿� ���� �� �̸� ����
        loadingUIManager.SetMapNameForScene("1.part0", "������ �αٽ�");
        loadingUIManager.SetMapNameForScene("2.part1", "����");
        loadingUIManager.SetMapNameForScene("4.part3", "����� �ӽ�ķ��");
        loadingUIManager.SetMapNameForScene("5.part4", "����� �ӽ�ķ��");
        loadingUIManager.SetMapNameForScene("6.part5", "���ڷ� ������ ��");
        loadingUIManager.SetMapNameForScene("7.part6", "��Ȳ��");
    }
}