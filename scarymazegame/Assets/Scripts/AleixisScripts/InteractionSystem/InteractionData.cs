//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : InteractionData.cs
// Description : holds all the get and set data functions for interactable objects
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact  
{
    [CreateAssetMenu(fileName =  "Interaction Data", menuName = "InteractionSystem/InteractionData")]  
     public class InteractionData : ScriptableObject
    {
        private InteractibleBase m_interactible;

        public InteractibleBase Interactible
        {
            get => m_interactible;
            set => m_interactible = value;
        }

        public void Interact() // calls on interact function from InteractibleBase
        {
            m_interactible.OnInteract();
            ResetData();
        }

        public bool isSameInteractible(InteractibleBase _newInteractible) => m_interactible == _newInteractible; // checks if the new interactible is the same as the old interactible
        public bool IsEmpty() => m_interactible == null; // checks if interactible is empty
        public void ResetData() => m_interactible = null;

    }
}