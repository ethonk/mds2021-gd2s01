//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : ItemScript.cs
// Description : Controls every item within the game. Gives attributes and respawns.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public enum ItemType
    {
        Trap,
        Craftable,
        Consumable,
        Shoppable
    };

    [Header("General")]
    public GameObject m_Player;
    public string itemName;
    [TextArea(3, 10)] // modifies the text area of the value in the inspector
    public string itemDescription;
    [TextArea(3, 10)]
    public string itemCraftReqs;
    public ItemType itemType;
    public int maxStack;

    [Header("Crafting Mats")]
    public List<GameObject> m_CraftingItems;

    [Header("Advanced - Do Not Modify")]
    public bool canBe_equipped;
    public bool canBe_consumed;
    public bool canBe_dropped;
    public bool canBe_crafted;
    
    public bool Craft(GlobalInventory _playerInventory)
    {
        if (m_CraftingItems.Count > 0)    // a craftable item.
        {
            print("Craftable!");
            // Check if both items exist in inventory
            foreach (GameObject item in m_CraftingItems)
            {

                if (!_playerInventory.SearchForItem(item)) return false;
            }

            print("Items exist");

            // If item exists but there is no space...
            for (int i = 0; i < _playerInventory.maxSpace; i++) 
            {
                if (_playerInventory.backpack[i] == gameObject && _playerInventory.backpackItemCount[i] == maxStack) return false;
                // Otherwise, add item to backpack
                else if (_playerInventory.backpack[i] == gameObject && _playerInventory.backpackItemCount[i] != maxStack)
                {
                    // Delete items
                    print("Deleting items...");
                    foreach (GameObject item in m_CraftingItems)
                    {
                        _playerInventory.DropItemGameObject(item);
                    }

                    _playerInventory.backpackItemCount[i]++;
                    print("Item should have been added");
                    return true;
                }
            }

            // If item does exist but theres no space...
            print("No space in inventory");
            if (_playerInventory.backpack.Count < maxStack) return false;

            // Otherwise... add this item to the inventory
            for (int i = 0; i < _playerInventory.backpack.Count; i++)
            {
                if (_playerInventory.backpack[i] == null)
                {
                    // Delete items
                    print("Deleting items...");
                    foreach (GameObject item in m_CraftingItems)
                    {
                        for (int j = 0; j < m_Player.GetComponent<GlobalInventory>().maxSpace; j++)
                        {
                            if (m_Player.GetComponent<GlobalInventory>().backpack[j].GetComponent<ItemScript>().itemName == item.GetComponent<ItemScript>().itemName)
                            {
                                m_Player.GetComponent<GlobalInventory>().backpackItemCount[j]--;
                                if (m_Player.GetComponent<GlobalInventory>().backpackItemCount[j] == 0) m_Player.GetComponent<GlobalInventory>().backpack[j] = null;
                            }
                        }
                    }

                    print("Instantiating...");
                    var craftedItem = Instantiate(gameObject);
                    _playerInventory.backpack[i] = craftedItem;
                    _playerInventory.backpackItemCount[i]++;

                    print("Item should be added!");
                    return true;
                }
            }
        }
        print("Not a craftable item...");
        return false;
    }

    void Start()
    {
        // Set item attributes
        switch (itemType)
        {
            case ItemType.Trap:
                canBe_equipped = true;
                canBe_consumed = false;
                canBe_dropped = true;
                canBe_crafted = true;
                break;
            case ItemType.Craftable:
                canBe_equipped = false;
                canBe_consumed = false;
                canBe_dropped = true;
                canBe_crafted = true;
                break;
            case ItemType.Consumable:
                canBe_equipped = false;
                canBe_consumed = true;
                canBe_dropped = true;
                canBe_crafted = true;
                break;
            case ItemType.Shoppable:
                canBe_equipped = true;
                canBe_consumed = true;
                canBe_dropped = true;
                canBe_crafted = true;
                break;
        }

        m_Player = GameObject.Find("Player");
    }
}
