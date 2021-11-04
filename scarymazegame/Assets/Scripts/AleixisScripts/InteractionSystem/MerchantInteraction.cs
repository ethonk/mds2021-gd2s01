using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
    public class MerchantInteraction : InteractibleBase
    {
        public Dialogue dialogue; 
        public GameObject playerCam;
        public GameObject merchantCam;

        public override void OnInteract()
        {
            base.OnInteract();

            playerCam.SetActive(!playerCam.activeInHierarchy);
            merchantCam.SetActive(!merchantCam.activeInHierarchy);

            GetComponent<CharacterMotor>().SwitchToShop();
        }
    }
}
