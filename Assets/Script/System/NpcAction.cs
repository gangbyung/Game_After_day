using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public static NpcAction Instance { get; private set; } // �̱��� �ν��Ͻ�

    public GameObject[] Npcs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �� ��ü�� �ı���
        }
    }

    public void NpcUnLock0() => Npcs[0].SetActive(true);
}