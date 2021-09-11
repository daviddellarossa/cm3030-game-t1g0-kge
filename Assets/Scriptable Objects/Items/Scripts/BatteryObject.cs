using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Battery Object", menuName = "Inventory System/Items/Battery")]
public class BatteryObject : ItemObject
{
    //amount of battery to restore
    public int batteryCharge;
    public void Awake()
    {
        //setting object type
        type = ItemType.Battery;
    }
}
