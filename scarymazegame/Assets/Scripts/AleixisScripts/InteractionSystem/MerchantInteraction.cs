using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class MerchantInteraction : InteractibleBase
    {
        public Dialogue dialogue; 

        public override void OnInteract()
        {
            base.OnInteract();

            FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
        }
    }
}
