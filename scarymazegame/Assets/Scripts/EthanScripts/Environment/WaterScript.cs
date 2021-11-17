//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : WaterScript.cs
// Description : Controls every water plane, reducing the speed of the player.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    float origSpeed = 6.0f;
    float waterSpeed = 3.0f;

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == "Player")
        {
            // Play water enter
            col.GetComponent<PlayerScript>().PlayAudio(col.GetComponent<PlayerScript>().waterEnter);

            col.transform.GetComponent<CharacterMotor>().m_MoveSpeed = waterSpeed;
        }   
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.name == "Player")
        {
            // Play water exit
            col.GetComponent<PlayerScript>().PlayAudio(col.GetComponent<PlayerScript>().waterExit);
            
            col.transform.GetComponent<CharacterMotor>().m_MoveSpeed = origSpeed;
        }   
    }
}
