using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventory : MonoBehaviour
{
    [Header("Backpack")]
    public GameObject backpackSlotContainer;
    public List<GameObject> backpack;
    public List<int> backpackItemCount;

    [Header("Backpack values")]
    public int maxSpace;

    public bool AddItem(GameObject item)
    {
        // Check if item already exists in backpack and can be stacked.
        for (int i = 0; i < maxSpace; i++)
        {
            // If item of same type exists and currently isn't at a max stack.
            if (backpack[i] == item && backpackItemCount[i] != item.GetComponent<ItemScript>().maxStack)
            {
                backpackItemCount[i] += 1;
                return true;
            }
        }

        // Find a spot to put new item in backpack.
        for (int i = 0; i < maxSpace; i++)
        {
            if (backpack[i] == null)
            {
                backpack[i] = item;
                backpackItemCount[i] += 1;
                return true;
            }
        }

        print("Item failed to add to inventory.");
        return false;
    }

    public void DropItem(string slotName)
    {
        // Get slot from name
        int slot = (int)char.GetNumericValue(slotName[slotName.Length-1]);

        if (backpackItemCount[slot] > 0)        // If the slot contains at least one item.
        {
            backpackItemCount[slot] -= 1;       // Reduce item by 1.
            
            if (backpackItemCount[slot] == 0)   // If the deletion caused the slot to be empty...
            {
                backpack[slot] = null;          // Slot is now empty, then proceed to delete item in slot.
                Destroy(backpackSlotContainer.transform.Find("Slot " + slot).GetChild(0).gameObject);
            }
        }
        LoadBackpack();                         // Update changes to backpack
    }

    public void LoadInSlot(GameObject item, Transform slot)      // Add a child inside slot
    {
        var newItem = Instantiate(item);                // Create item
        newItem.transform.position = slot.position;     // Set position
        newItem.transform.SetParent(slot);              // Set parent
    }

    public void DestroyInSlot(Transform slot)   // Destroy all children in a slot.
    {
        if (slot.GetComponentsInChildren<Transform>().Length > 0)   // If slot isn't empty.
        {
            foreach (Transform child in slot)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void LoadBackpack()
    {
        for (int i = 0; i < maxSpace; i++)
        {
            if (backpack[i] != null)
            {
                DestroyInSlot(backpackSlotContainer.transform.Find("Slot " + i));                   // Destroy item inside
                LoadInSlot(backpack[i], backpackSlotContainer.transform.Find("Slot " + i));         // Load item in slot
            }
            else
            {
                DestroyInSlot(backpackSlotContainer.transform.Find("Slot " + i));  // Destroy item inside
            }
        }
    }

    private void Start()
    {
        // Allocate max space.
        maxSpace = backpackSlotContainer.transform.childCount;

        for (int i = 0; i < maxSpace; i++)
        {
            backpack.Add(null);
            backpackItemCount.Add(0);
        }
        
        LoadBackpack();
    }
}
