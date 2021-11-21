//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : MerchantInteraction.cs
// Description : Derived script from interactible base that controls the merchant interaction
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class MerchantInteraction : InteractibleBase
    {
        [Header("References")]
        public Dialogue dialogue; 
        public Camera merchantCam;
        
        [Header("Player")]
        public GameObject Player;
        public CharacterMotor m_PlayerMotor;
        public Camera playerCam;

        [Header("Audio")]
        public AudioClip talkSound;

        public override void OnInteract()
        {
            // sound play
            if (Player.GetComponent<PlayerScript>().cameraState != PlayerScript.CameraState.shop) GetComponent<AudioSource>().PlayOneShot(talkSound);

            base.OnInteract();

            m_PlayerMotor.playerLock = true;
            Cursor.lockState =  CursorLockMode.None;
            Player.GetComponent<MouseLook>().m_CameraLock = true;

            merchantCam.gameObject.SetActive(!merchantCam.gameObject.activeInHierarchy);
            playerCam.gameObject.SetActive(!playerCam.gameObject.activeInHierarchy);

            // Set player cam to shop
            Player.GetComponent<PlayerScript>().cameraState = PlayerScript.CameraState.shop;
            GetComponent<GlobalInventory>().LoadBackpack();
        }

        public void Start()
        {
            // Player
            Player = GameObject.Find("Player");
            m_PlayerMotor = Player.GetComponent<CharacterMotor>();
            playerCam = Player.transform.Find("Main Camera").GetComponent<Camera>();

            // Merchant
            merchantCam = transform.Find("MerchantCamera").GetComponent<Camera>();
        }
    }   
}
