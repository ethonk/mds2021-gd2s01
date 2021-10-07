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
                Debug.Log("INTERACTED: " + gameObject.name);
            }
        #endregion
    }
}



