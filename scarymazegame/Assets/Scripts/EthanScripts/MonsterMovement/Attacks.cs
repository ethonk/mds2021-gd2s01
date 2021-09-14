using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public bool eaten = false;

    public void EatAttack(Transform player, Transform mouthPoint)
    {
        eaten = true;
        player.gameObject.GetComponent<CharacterMotor>().playerLock = true;     // Lock player in place.
        player.transform.parent = mouthPoint.parent;
        player.position = mouthPoint.position;                                  // Sets player position.
        player.gameObject.GetComponent<PlayerScript>().TakeDamage(50f);          // Damage
    }
}
