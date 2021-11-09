using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class MerchantInteraction : InteractibleBase
    {
        [Header("References")]
        public Dialogue dialogue; 
        public GameObject playerCam;
        public GameObject merchantCam;
        public GameObject Player;
        public CharacterMotor m_PlayerMotor;

        public override void OnInteract()
        {
            base.OnInteract();

            m_PlayerMotor.playerLock = true;
            Cursor.lockState =  CursorLockMode.None;
            print(Cursor.lockState);

            merchantCam.SetActive(!merchantCam.activeInHierarchy);
            playerCam.SetActive(!playerCam.activeInHierarchy);
        }

        public void Update()
        {
            GetComponent<GlobalInventory>().LoadBackpack();
        }
    }
}
