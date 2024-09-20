using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Dictionary<string, GameObject> npcFKeyDictionary;

    public GameObject[] Npc_F_key;

    void Start()
    {
        if (Npc_F_key == null || Npc_F_key.Length == 0)
        {
            return;
        }

        npcFKeyDictionary = new Dictionary<string, GameObject>();

        for (int i = 0; i < Npc_F_key.Length; i++)
        {
            string npcName = "NPC" + (i + 1).ToString("D2");  
            npcFKeyDictionary.Add(npcName, Npc_F_key[i]);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player collided with {gameObject.name}");

            if (npcFKeyDictionary.TryGetValue(gameObject.name, out GameObject npcFKey))
            {
               
                npcFKey.SetActive(true);
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            if (npcFKeyDictionary.TryGetValue(gameObject.name, out GameObject npcFKey))
            {

                npcFKey.SetActive(false);
            }
            
        }
    }
}
