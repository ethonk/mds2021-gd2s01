//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : InteractionInputData.cs
// Description : this script is responsible for collecting all the data from reading player inputs
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    [CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]

    public class InteractionInputData : ScriptableObject
    {
        [Header ("variables")]
        private bool m_interactClicked;  // if public, used for debugging in the inspector.
        private bool m_interactReleased;

        public bool InteractClicked // getter and setter for private variable m_InteractClicked
        {
            get => m_interactClicked;
            set => m_interactClicked = value;
        }

        public bool InteractReleased    // getter and setter for the private varaible m_InteractReleased
        {
            get => m_interactReleased;
            set => m_interactReleased = value;
        }

        public void Reset() // defaults everything to false
        {
            m_interactClicked = false;
            m_interactReleased = false;
        }

    }
}

