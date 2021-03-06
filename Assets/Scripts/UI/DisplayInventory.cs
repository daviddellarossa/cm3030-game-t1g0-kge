using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int X_SPACE_BETWEN_ITEM;
    public int X_START;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int Y_START;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    //creates the inventory display
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentsInChildren<TextMeshProUGUI>()[0].text = slot.amount.ToString("n0");
            obj.GetComponentsInChildren<TextMeshProUGUI>()[1].text = (i + 1).ToString("n0");
            itemsDisplayed.Add(slot, obj);
        }
    }

    //get the local position
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEN_ITEM * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    //update display if inventory changes
    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                //update number by sprite to show amount of item
                itemsDisplayed[slot].GetComponentsInChildren<TextMeshProUGUI>()[0].text = slot.amount.ToString("n0");
            }
            else
            {
                //create new item
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentsInChildren<TextMeshProUGUI>()[0].text = inventory.Container.Items[i].amount.ToString("n0");
                obj.GetComponentsInChildren<TextMeshProUGUI>()[1].text = (i + 1).ToString("n0");
                itemsDisplayed.Add(inventory.Container.Items[i], obj);
            }
        }
    }

    //use the item in an inventory slot
    public void UseItem(Item item, int amount)
    {
        if (item.Id == 0)
        {
            Debug.Log("This is a BandAid");
            Debug.Log("Amount: " + amount);
            amount -= 1;
            Debug.Log("Amount: " + amount);

        }
        if (item.Id == 1)
        {
            Debug.Log("This is a Health Potion");
        }
        if (item.Id == 2)
        {
            Debug.Log("This is a key.");
        }
    }
}
