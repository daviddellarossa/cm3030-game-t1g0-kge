using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Pack Object", menuName = "Inventory System/Items/Health Pack")]
public class HealthPackObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemType.HealthPack;
    }
}
