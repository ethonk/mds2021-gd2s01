//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : ItemInteractable
// Description : the base script for an item that is interactable
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class ItemInteractable : InteractibleBase
    {
        public override void OnInteract()
        {
            if (gameObject.transform.parent == null)
            {
                // Run on interaction
                base.OnInteract();

                // Add to inventory
                // 1) is there already a max stack of that item inside the inventory
                var newItem = Instantiate(gameObject);
                newItem.name = gameObject.name;
                newItem.SetActive(true);

                if(player.GetComponent<GlobalInventory>().AddItem(newItem))
                {
                    // 2) if not, continue
                    gameObject.SetActive(false); // original item
                    newItem.transform.parent = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash");
                    newItem.transform.position = player.GetComponent<GlobalInventory>().backpackSlotContainer.transform.parent.Find("trash").position;
                } 
                else
                {  
                    print("Can't add item, stack maxed.");
                    // 2.5) if it is, destroy
                    Destroy(newItem);
                }
            }
        }
    }
}

