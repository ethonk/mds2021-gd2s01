using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public WeaponScript weaponScript;
    public Rigidbody rb;
    public BoxCollider col;
    public Transform player, weaponContainer, cam;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private void Start()
    {
        // Setup
        if (!equipped)
        {
            weaponScript.enabled = false;
            rb.isKinematic = false;
            col.isTrigger = false;
        }
        if (equipped)
        {
            weaponScript.enabled = true;
            rb.isKinematic = true;
            col.isTrigger = true;
            slotFull = true;
        }
    }
    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;    // Get distance of weapon from player

        // If player is in range and E is pressed.
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        // If player presses Q with an item equipped.
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // Make weapon child of the camera and move it to default position.
        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // Make rigidbody kinematic and boxcollider a trigger.
        rb.isKinematic = true;
        col.isTrigger = true;

        // Enable weapon script
        weaponScript.enabled = true;

        // Run weaponscript pickup function
        weaponScript.PickedUp();
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        rb.isKinematic = false;
        col.isTrigger = false;

        // Weapon carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        // Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        // For realism add random rotation
        float randomRot = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(randomRot,randomRot,randomRot) * 10);

        weaponScript.enabled = false;
    }
}
