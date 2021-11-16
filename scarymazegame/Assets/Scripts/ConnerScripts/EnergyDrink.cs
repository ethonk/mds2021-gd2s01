//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : EnergyDrink
// Description : The main function for Battleships.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    public GameObject Player;
   
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Player.GetComponent<PlayerScript>().stamina = 100;
            Destroy(gameObject);
        }
    }
}
