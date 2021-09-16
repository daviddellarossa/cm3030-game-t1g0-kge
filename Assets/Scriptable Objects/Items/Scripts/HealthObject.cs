using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Object", menuName = "Inventory System/Items/Health Object")]
public class HealthObject : ItemObject
{
    //amount of health to restore
    public int restoreHealthValue;
    //setting object type
    public void Awake()
    {
        type = ItemType.Health;
    }
}
