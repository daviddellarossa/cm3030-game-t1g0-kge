using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows to create object from editor
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        //seting object type
        type = ItemType.Default;
    }
}
