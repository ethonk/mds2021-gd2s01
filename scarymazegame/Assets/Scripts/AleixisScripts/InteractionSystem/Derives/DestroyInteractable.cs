//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : DestroyInteractable.cs
// Description : derived script from interactible base that destroys an object
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
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

