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
        }
    }
}

