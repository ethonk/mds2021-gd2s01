using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health -= 1;
        }
    }
}
