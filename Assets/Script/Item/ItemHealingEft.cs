using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healingPoint = 0;
    public float healingHp;
    public override bool ExecuteRole()
    {
        Debug.Log("playerHp add : " + healingPoint);
        Hud.Instance.currentHp += 20;
        return true;
    }
}
