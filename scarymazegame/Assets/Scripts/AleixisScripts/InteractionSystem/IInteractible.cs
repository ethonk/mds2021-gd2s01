using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public interface IInteractible 
    {   
        float HoldDuration { get; } // capital letters because interface only accepts properties not varaibles.

        bool HoldInteract { get; }

        bool MultipleUse { get; }

        bool IsInteractible { get; }

        bool DestroyObj { get; }

        void OnInteract(); // function for what happens when you interact
                           // empty method
    }
}