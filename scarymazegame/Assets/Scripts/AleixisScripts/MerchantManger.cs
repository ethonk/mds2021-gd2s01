using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantManger : MonoBehaviour
{
    [Header ("References")]
    GameObject _PlayerRef; // creates a gameobject playeref, blank for now

    [Header ("Transformations")]
    public Transform Merchant;

    [Header ("Variables")]
    public float DetectRadius = 5f;

    [Header("Audio")]
    public AudioClip teleportIn;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerRef = GameObject.Find("Player"); // tells the script to find the game object called Player and assigns it to the GameObject playerRef for reference

        Vector3 PlayerPosition = new Vector3(_PlayerRef.transform.position.x, // line creates a new vector3 
                                                transform.position.y, // locks the Y-axis of the rotation
                                                _PlayerRef.transform.position.z);

        transform.LookAt(PlayerPosition);
        
    }
}
