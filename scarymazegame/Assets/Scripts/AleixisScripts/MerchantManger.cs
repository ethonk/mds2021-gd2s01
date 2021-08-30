using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantManger : MonoBehaviour
{
    [Header ("References")]
    GameObject _PlayerRef; // creates a gameobject playeref, blank for now

    [Header ("Transformations")]
    public Transform Merchant;

    // Update is called once per frame
    void Update()
    {
        _PlayerRef = GameObject.Find("Player"); // tells the script to find the game object called Player and assigns it to the GameObject playerRef for reference

        Vector3 PlayerPosition = new Vector3(_PlayerRef.transform.position.x,
                                                transform.position.y,
                                                _PlayerRef.transform.position.z);

        transform.LookAt(PlayerPosition);
        
    }
}
