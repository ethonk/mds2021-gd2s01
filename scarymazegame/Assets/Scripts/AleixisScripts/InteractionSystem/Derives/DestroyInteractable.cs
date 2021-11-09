using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class DestroyInteractable : InteractibleBase
    {

        public override void OnInteract() // overrides the virtual void that is in InteractibleBase.cs
        {
            base.OnInteract();

            Destroy(gameObject);
        }
    
    }   

}

