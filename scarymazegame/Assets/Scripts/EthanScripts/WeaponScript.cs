using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    AudioSource audioData;

    public void PickedUp()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }
}
