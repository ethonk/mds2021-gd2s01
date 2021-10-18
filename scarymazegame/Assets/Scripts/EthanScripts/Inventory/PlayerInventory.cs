using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Backpack")]
    public List<GameObject> backpack;
    public List<int> backpackItemCount;

    [Header("Backpack values")]
    public int maxSpace = 6;

    public void Start()
    {
        for (int i = 0; i < maxSpace; i++)
        {
            backpack.Add(null);
            backpackItemCount.Add(0);
        }
    }

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
}
