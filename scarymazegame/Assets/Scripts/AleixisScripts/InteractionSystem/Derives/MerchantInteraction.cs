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

        public override void OnInteract()
        {
            base.OnInteract();

            m_PlayerMotor.playerLock = true;
            Cursor.lockState =  CursorLockMode.None;
            print(Cursor.lockState);

            merchantCam.gameObject.SetActive(!merchantCam.gameObject.activeInHierarchy);
            playerCam.gameObject.SetActive(!playerCam.gameObject.activeInHierarchy);

            // Set player cam to shop
            Player.GetComponent<PlayerScript>().cameraState = PlayerScript.CameraState.shop;
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

        public void Update()
        {
            GetComponent<GlobalInventory>().LoadBackpack();
        }
    }
}
