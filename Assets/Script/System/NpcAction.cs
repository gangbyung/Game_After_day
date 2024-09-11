using System.Collections;
using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public static NpcAction Instance { get; private set; } // 싱글턴 인스턴스

    public GameObject[] Npcs;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    Coroutine NpcLock51;
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
    void Start()
    {
        
    }

    public void NpcUnLock5() => Npcs[0].SetActive(true);
    public void NpcLock5() => Npcs[0].SetActive(false);
    public void NpcUnLock5_1()
    {
       NpcLock51 = StartCoroutine(NpcUnLock5_1cor());
    }
    public void NpcUnLock5_2() => Npcs[2].SetActive(true);
    public void NpcDumUnLock()
    {
        Npcs[3].SetActive(true);
    }

    public void NpcUnLock13()
    {
        Npcs[0].SetActive(false);
        Npcs[1].SetActive(true);
    }

    public void NpcUnLock14()
    {
        Npcs[0].SetActive(false);

        Npcs[2].SetActive(true);
    }
    public void RadiUnLock()
    {
        Npcs[0].SetActive(true);
    }
    public void NpcUnLock16()
    {
        StartCoroutine(NpcUnLock16cor());
    }
    IEnumerator NpcUnLock5_1cor()
    {
        Npcs[1].SetActive(true);
        yield return new WaitForSeconds(.1f);
        Npcs[1].SetActive(false);
        yield return new WaitForSeconds(.1f);
        Npcs[1].SetActive(true);
        yield return null;
    }
    IEnumerator NpcUnLock16cor()
    {
        Npcs[0].SetActive(false);
        Npcs[1].SetActive(true);
        yield return new WaitForSeconds(.1f);
        Npcs[1].SetActive(false);
        yield return new WaitForSeconds(.1f);
        Npcs[1].SetActive(true);
        yield return null;
    }
}