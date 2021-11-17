//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : EnergyDrink
// Description : The script for the stamina from energy drinks.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    public GameObject Player;
    
    void Start()
    {
        Player = GameObject.Find("Player");
    }
 
    // Update is called once per frame
    public void Drink()
    {
        Player.GetComponent<PlayerScript>().stamina = Player.GetComponent<PlayerScript>().stamina_max;
    }
}
