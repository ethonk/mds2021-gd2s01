//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : Pickup.cs 
// Description : Controls the physical pickup and drop of an object with a RigidBody2D 
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        // Setup
        if (!equipped)
        {
            GetComponent<WeaponScript>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().isTrigger = false;
        }
        if (equipped)
        {
            GetComponent<WeaponScript>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().isTrigger = true;
            slotFull = true;
        }
    }
    private void Update()
    {
        Vector3 distanceToPlayer = GameObject.Find("Player").transform.position - transform.position;    // Get distance of weapon from player
        
        // If player presses Q with an item equipped.
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        equipped = true;
        slotFull = true;

        // Make weapon child of the camera and move it to default position.
        transform.SetParent(GameObject.Find("Player").transform.Find("Main Camera").transform.Find("GunContainer"));

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        // Make rigidbody kinematic and boxcollider a trigger.
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().isTrigger = true;

        // Enable weapon script
        GetComponent<WeaponScript>().enabled = true;

        // Run weaponscript pickup function
        GetComponent<WeaponScript>().PickedUp();
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<BoxCollider>().isTrigger = false;

        // Add force
        GetComponent<Rigidbody>().AddForce(GameObject.Find("Player").transform.Find("Main Camera").forward * dropForwardForce, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(GameObject.Find("Player").transform.Find("Main Camera").up * dropUpwardForce, ForceMode.Impulse);

        GetComponent<WeaponScript>().enabled = false;
    }
}
