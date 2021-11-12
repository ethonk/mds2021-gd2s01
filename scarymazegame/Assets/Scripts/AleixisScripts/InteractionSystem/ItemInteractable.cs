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
            player.GetComponent<GlobalInventory>().AddItem(gameObject);
            gameObject.transform.parent = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash");
            gameObject.transform.position = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash").position;
        }
    }
}

