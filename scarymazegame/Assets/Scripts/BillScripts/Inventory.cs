using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightClickEvent;

    private void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)        //Every item we have this will assign it to an item slot
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)                           //For every item slot that is empty this will set the item to null
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)                                  //Adding item check full or not
    {
        if (IsFull())
        {
            return false;
        }
        else 
        {
            items.Add(item);
            RefreshUI();
            return true;
        }
    }

    public bool RemoveItem(Item item)                               //Removing items
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()                                        //Checks if inv is full by counting if there are more or equal amounts items than the itemSlots
    {
        return items.Count >= itemSlots.Length;

    }

}
