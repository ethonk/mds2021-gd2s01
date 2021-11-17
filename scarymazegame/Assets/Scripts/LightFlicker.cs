//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : LightFlicker.cs
// Description : Dynamic light flashing for the merchant's lantern.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip flicker_off;
    public AudioClip flicker_on;

    public IEnumerator Flicker()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<Light>().intensity = 0;
        transform.parent.Find("Lamp").GetComponent<MeshRenderer>().enabled = false;

        // play sound if close to plr
        if (Vector3.Distance(GameObject.Find("Merchant").transform.position, GameObject.Find("Player").transform.position) < 14)
        {
            GetComponent<AudioSource>().PlayOneShot(flicker_off);
        }
    
        StartCoroutine(UnFlicker());
    }

    public IEnumerator UnFlicker()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Light>().intensity = 10;
        transform.parent.Find("Lamp").GetComponent<MeshRenderer>().enabled = true;
        
        // play sound if close to plr
        if (Vector3.Distance(GameObject.Find("Merchant").transform.position, GameObject.Find("Player").transform.position) < 14)
        {
            GetComponent<AudioSource>().PlayOneShot(flicker_on);
        }

        StartCoroutine(Flicker());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flicker());
    }
}
