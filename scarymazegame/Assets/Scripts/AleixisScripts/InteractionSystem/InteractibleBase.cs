using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class InteractibleBase : MonoBehaviour, IInteractible   // base class that all interactible items derive from
    {
        #region Variables
            [Header ("Interact Settings")]
            public float holdDuration;
        
            public bool holdInteract;
            public bool multipleUse;
            public bool isInteractible;

        public Item item;



        #endregion
      
        #region Properties
            public float HoldDuration => holdDuration;

            public bool HoldInteract => holdInteract;

            public bool MultipleUse => multipleUse;

            public bool IsInteractible => isInteractible;
        #endregion 

        #region Classes
            public virtual void OnInteract()
            {
            //Debug.Log("INTERACTED: " + gameObject.name);
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
        #endregion
    }
}



