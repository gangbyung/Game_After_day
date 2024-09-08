using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.color = new Color (255,255,255);
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.color = new Color(20,20,20);
        itemIcon.gameObject.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();
        if (isUse)
        {
            Inventory.instance.RemoveItem(slotnum);
        }
    }
}
