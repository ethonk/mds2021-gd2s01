using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class ItemInteractable : InteractibleBase
    {
        public override void OnInteract()
        {
            // Run on interaction
            base.OnInteract();

            // Add to inventory
            gameObject.SetActive(false); // original item

            var newItem = Instantiate(gameObject);
            newItem.name = gameObject.name;
            newItem.SetActive(true);
            player.GetComponent<GlobalInventory>().AddItem(newItem);
            
            newItem.transform.parent = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash");
            newItem.transform.position = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash").position;
        }
    }
}

