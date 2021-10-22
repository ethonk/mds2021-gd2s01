using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInteraction : MonoBehaviour
{
    
    public Rigidbody rb;
    public BoxCollider col;
    public Transform player, TrapContainer, cam;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool Activated;
    public bool equipped;
    public static bool slotFullT;


    private void Start()
    {
        Activated = false;
        // Setup
        if (!equipped)
        {
            
            rb.isKinematic = false;
            col.isTrigger = false;
        }
        if (equipped)
        {
            
            rb.isKinematic = true;
            col.isTrigger = true;
            slotFullT = true;
        }
    }
    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;    // Get distance of weapon from player

        // If player is in range and E is pressed.
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFullT)
        {
            PickUp();
        }

        // If player presses Q with an item equipped.
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        if (equipped && Input.GetKeyDown(KeyCode.F))
        {
            Activated = true;
        }
        if (!equipped && Activated)
        {
            Activation();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFullT = true;

        // Make weapon child of the camera and move it to default position.
        transform.SetParent(TrapContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make rigidbody kinematic and boxcollider a trigger.
        rb.isKinematic = true;
        col.isTrigger = true;

        

        
    }

    private void Drop()
    {
        equipped = false;
        slotFullT = false;

        // Set parent to null
        transform.SetParent(null);

        rb.isKinematic = false;
        col.isTrigger = false;

        // Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        // For realism add random rotation
       
        
        //float randomRot = Random.Range(-1f, 1f);
        //rb.AddTorque(new Vector3(randomRot, randomRot, randomRot) * 10);

        
    }

    void Activation()
    {
        //Traps monster
        Destroy(gameObject, 5);
    }
}
