using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Dictionary<string, GameObject> npcFKeyDictionary;

    // Public field for assigning in the Unity Inspector
    public GameObject[] Npc_F_key;

    void Start()
    {
        // Check if Npc_F_key array is properly assigned
        if (Npc_F_key == null || Npc_F_key.Length == 0)
        {
            // Debug.LogError("Npc_F_key array is not assigned in the Inspector.");
            return;
        }

        npcFKeyDictionary = new Dictionary<string, GameObject>();

        // Initialize the dictionary based on the available GameObjects
        for (int i = 0; i < Npc_F_key.Length; i++)
        {
            string npcName = "NPC" + (i + 1).ToString("D2");  // Format to match "NPC01", "NPC02", ..., "NPC10", etc.
            // Debug.Log($"Mapping {npcName} to Npc_F_key[{i}]");
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
               
                // Debug.Log($"{gameObject.name} found in dictionary. Activating associated GameObject.");
                npcFKey.SetActive(true);
            }
            else
            {
                // Debug.LogError($"{gameObject.name} not found in dictionary!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log($"Player exited collision with {gameObject.name}");

            if (npcFKeyDictionary.TryGetValue(gameObject.name, out GameObject npcFKey))
            {
                // Debug.Log($"{gameObject.name} found in dictionary. Deactivating associated GameObject.");
                npcFKey.SetActive(false);
            }
            else
            {
                // Debug.LogError($"{gameObject.name} not found in dictionary!");
            }
        }
    }
}
