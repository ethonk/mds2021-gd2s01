using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;                                       //static inventory called instance -variable shared by all instances of a class

    void Awake()                                                    // Upon start we making the instance = this (Means that every instance can access this inv)
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inv found!");
            return;
        }
        instance = this;
    }
    #endregion                                                                                                                  

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int invSlotSpace = 10;

    //ItemDefinition is under a scriptable object under ItemDefinition.cs and is an ingame item 
    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= invSlotSpace)
            {
                Debug.Log("Not enough space in inventory");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();                                   //triggering the onItemChagnedCallBack
            }

        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();                                   //triggering the onItemChagnedCallBack
        }
    }
}
