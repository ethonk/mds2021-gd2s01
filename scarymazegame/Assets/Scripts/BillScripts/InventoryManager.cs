using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickEvent += EquipFromInventory;
    }

    private void EquipFromInventory(Item item)
    {
        if (item is EquippableItems)
        {
            Equip((EquippableItems)item);                           //Allows you to drag n equip equippable items
        }
    }

    public void Equip(EquippableItems item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItems previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);                                //Returns item back into inventory
            }
        }
    }

    public void Unequip(EquippableItems item)                           //checks if inventory is full first before returning item back into inv
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))     //Removes from panel back into inv
        {
            inventory.AddItem(item);
        }
    }

}
