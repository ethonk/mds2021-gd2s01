using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public bool eaten = false;

    public void SlashAttack(PlayerScript player)
    {
        // play hurt sound
        player.GetComponent<AudioSource>().PlayOneShot(player.pain_heavy);
        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().slashSound);

        player.TakeDamage(20f); // damage
    }

    public void EatAttack(Transform player, Transform mouthPoint)
    {
        // play hurt sound
        player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerScript>().pain_heavy);

        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().eatSound);

        // get player orig transform
        Vector3 plrOrigPosition = player.transform.position;

        eaten = true;
        player.gameObject.GetComponent<CharacterMotor>().playerLock = true;      // Lock player in place.
        player.transform.parent = mouthPoint.parent;
        player.position = mouthPoint.position;                                   // Sets player position.
        player.gameObject.GetComponent<PlayerScript>().TakeDamage(50f);          // Damage

        StartCoroutine(Spit(player, plrOrigPosition, 3.0f));
    }

    IEnumerator Spit(Transform player, Vector3 _origPos, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // play sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<MonsterDetails>().spitSound);

        // Add impulse
        player.transform.position = _origPos;

        eaten = false;
        player.gameObject.GetComponent<CharacterMotor>().playerLock = false;     // Unlock player.
        player.transform.parent = null;
        player.gameObject.GetComponent<PlayerScript>().TakeDamage(10f);          // Damage
    }
}
