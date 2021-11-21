//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : MerchantManager.cs
// Description : Manages some of the merchants mechanics mainly regarding position
// Author : Aliexis Alvarez
// Mail : Aliexis.Alvarez@mediadesignschool.com
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantManger : MonoBehaviour
{
    [Header ("References")]
    GameObject _PlayerRef; // creates a gameobject playeref, blank for now
    public Transform Child;

    [Header ("Transformations")]
    public Transform Merchant;

    [Header ("Variables")]
    public float DetectRadius = 5f;

    [Header("Audio")]
    public AudioClip teleportIn;
    public AudioClip teleportOut;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
    }

    public void Start()
    {
        _PlayerRef = GameObject.Find("Player"); // tells the script to find the game object called Player and assigns it to the GameObject playerRef for reference
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 PlayerPosition = new Vector3(_PlayerRef.transform.position.x, // line creates a new vector3 
                                                transform.position.y, // locks the Y-axis of the rotation
                                                _PlayerRef.transform.position.z);

        transform.LookAt(PlayerPosition);
    }
}
