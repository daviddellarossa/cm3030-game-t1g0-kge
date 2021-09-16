using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    //pick up object and add to inventory
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
}
