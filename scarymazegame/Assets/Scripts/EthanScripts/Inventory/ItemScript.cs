using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public enum ItemType
    {
        Trap,
        Craftable,
        Consumable
    };

    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public int maxStack;
}
