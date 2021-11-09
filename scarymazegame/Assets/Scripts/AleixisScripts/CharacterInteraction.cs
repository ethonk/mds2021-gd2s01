using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------ WARNING ----------------
// I think this script is not in use anymore
// im not too sure but I think this is old
// ------------------ WARNING ----------------

public class CharacterInteraction : MonoBehaviour
{
    [Header ("Declaring Variables")]
    public bool InRange;

    // Merchant Interaction
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Merchant"))
        {
            InRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Merchant"))
        {
            InRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(InRange);
        if (InRange && Input.GetKey(KeyCode.E))
        {
            Debug.Log("The merchant is staring at you");
        }
    }
}
