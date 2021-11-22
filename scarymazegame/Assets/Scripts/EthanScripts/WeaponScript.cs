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

    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(hitCooldown);
        canUse = true;
    }

    public void PickedUp()
    {
    }

    public void Attack()
    {
        // Start cooldown
        StartCoroutine(Cooldown());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canUse)
        {
            Attack();
        }
    }
}
