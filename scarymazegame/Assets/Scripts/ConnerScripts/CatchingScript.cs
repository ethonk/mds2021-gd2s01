//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : CatchingScript
// Description : The script for catching monsters.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CatchingScript : MonoBehaviour
{
    public MonsterDetails EnemyDetails;
    public GameObject Player;

    public float Chance;
    //public static Random RndNumber;
    public float NumberGenerated;

    bool hashit = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Monster" && hashit == false)
        {
            hashit = true;
            GameObject Enemy = hit.transform.root.gameObject;

            // Define enemy details
            EnemyDetails = hit.gameObject.GetComponent<MonsterDetails>();

            Chance = 100 - Enemy.GetComponent<MonsterDetails>().health;
            NumberGenerated = Random.Range(1, 100);
            
            if (NumberGenerated <= Chance)
            {
                print("caught!");
                Player.GetComponent<PlayerScript>().MonsterCount += 1;
                Destroy(Enemy);
            }
            else 
            {
                print("miss!");
                Enemy.GetComponent<MonsterDetails>().health += 20;
                if (Enemy.GetComponent<MonsterDetails>().health > 100)
                {
                    Enemy.GetComponent<MonsterDetails>().health = 100;
                }
            }

            // After hit, destroy.
            Destroy(gameObject);
        }
    }

}
