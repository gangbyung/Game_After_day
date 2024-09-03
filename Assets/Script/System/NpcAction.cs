using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAction : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Npcs;

    public void NpcUnLock() => Npcs[0].SetActive(true);
    
}
