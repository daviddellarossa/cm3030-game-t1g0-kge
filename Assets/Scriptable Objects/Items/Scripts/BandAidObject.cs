using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BandAid Object", menuName = "Inventory System/Items/BandAid")]
public class BandAidObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemType.BandAid;
    }
}
