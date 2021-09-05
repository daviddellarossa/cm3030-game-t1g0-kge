using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion Object", menuName = "Inventory System/Items/Health Potion")]
public class HealthPotionObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemType.HealthPotion;
    }
}
