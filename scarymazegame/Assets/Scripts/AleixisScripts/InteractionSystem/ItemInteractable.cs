using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class ItemInteractable : InteractibleBase
    {
        #region variables
        public Item item;
        #endregion

        public override void OnInteract()
        {
            base.OnInteract();

            switch (GetComponent<Item>().ItemType)
            {
            #region Weapon
            case Category.Weapon:
             Debug.Log("Equipting weapon");
                    
                break;
            #endregion

            #region Craftable
            case Category.Craftable:
                Debug.Log("Picking up Craftable item");
                    
                break;
            #endregion

            #region Tools
            case Category.Tools:
                Debug.Log("Picking up Tool item");
                    
                break;
            #endregion

            #region Resource
            case Category.Resource:
                Debug.Log("Picking up Resource item");
                bool wasPickedUp = Inventory.instance.Add(item);

                if (wasPickedUp)
                {
                    Destroy(gameObject);
                }

                break;
            #endregion
            
            }
        }
    }
}

