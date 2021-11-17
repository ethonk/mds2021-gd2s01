//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : WeaponScript.cs
// Description : Controls any weapon-type object.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Weapon Values")]
    public bool canUse = true;
    public float hitCooldown;

    [Header("Audio")]
    public AudioClip equipSound;
    public AudioClip attackSound;

    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(hitCooldown);
        canUse = true;
    }

    public void PickedUp()
    {
        GetComponent<AudioSource>().PlayOneShot(equipSound);
    }

    public void Attack()
    {
        // Start cooldown
        StartCoroutine(Cooldown());
        
        GetComponent<AudioSource>().PlayOneShot(attackSound);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canUse)
        {
            Attack();
        }
    }
}
