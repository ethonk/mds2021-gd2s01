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
