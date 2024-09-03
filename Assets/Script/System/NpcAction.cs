using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public static NpcAction Instance { get; private set; } // 싱글턴 인스턴스

    public GameObject[] Npcs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 이 객체를 파괴함
        }
    }

    public void NpcUnLock0() => Npcs[0].SetActive(true);
}