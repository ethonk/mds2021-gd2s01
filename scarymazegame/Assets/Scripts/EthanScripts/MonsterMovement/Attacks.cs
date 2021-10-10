using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public bool eaten = false;

    
    public void EatAttack(Transform player, Transform mouthPoint)
    {
        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().eatSound);

        eaten = true;
        player.gameObject.GetComponent<CharacterMotor>().playerLock = true;     // Lock player in place.
        player.transform.parent = mouthPoint.parent;
        player.position = mouthPoint.position;                                   // Sets player position.
        player.gameObject.GetComponent<PlayerScript>().TakeDamage(50f);          // Damage

        StartCoroutine(Spit(player, 3.0f));
    }

    IEnumerator Spit(Transform player, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().spitSound);

        eaten = false;
        player.gameObject.GetComponent<CharacterMotor>().playerLock = false;     // Unlock player.
        player.transform.parent = null;
        player.gameObject.GetComponent<PlayerScript>().TakeDamage(10f);          // Damage

        // Add impulse
        player.transform.position += player.transform.Find("Main Camera").forward*10;
    }
}
