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

    public Vector3 defaultSize;

    private void Start()
    {
        // Initialize vars
        player = GameObject.Find("Player").transform;
        weaponScript = GetComponent<WeaponScript>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        cam = player.Find("Main Camera");
        weaponContainer = cam.Find("GunContainer");
        defaultSize = transform.localScale;

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

        // Make rigidbody kinematic and boxcollider a trigger.
        rb.isKinematic = true;
        col.isTrigger = true;

        // Enable weapon script
        weaponScript.enabled = true;

        // Run weaponscript pickup function
        weaponScript.PickedUp();

        // Change scale
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        rb.isKinematic = false;
        col.isTrigger = false;

        // Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

        weaponScript.enabled = false;

        // Reset scale
        StartCoroutine(ResetScale(0.5f));
    }

    private IEnumerator ResetScale(float time)
    {
        yield return new WaitForSeconds(time);
        transform.localScale = defaultSize;
    }
}
