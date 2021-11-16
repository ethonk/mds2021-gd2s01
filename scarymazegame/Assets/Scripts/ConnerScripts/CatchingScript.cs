//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : CatchingScript
// Description : The main function for Battleships.
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

    // Start is called before the first frame update
    void Start()
    {
       
        EnemyDetails = GetComponent<MonsterDetails>();
        GameObject Enemy;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Monster")
        {
            GameObject Enemy = hit.transform.gameObject;
            Chance = 100 - Enemy.GetComponent<MonsterDetails>().health;
            NumberGenerated = Random.Range(1, 100);
            if (NumberGenerated <= Chance)
            {
                Player.GetComponent<PlayerScript>().MonsterCount += 1;
                Destroy(Enemy);
            }
            else 
            {
                Enemy.GetComponent<MonsterDetails>().health += 20;
                if (Enemy.GetComponent<MonsterDetails>().health > 100)
                {
                    Enemy.GetComponent<MonsterDetails>().health = 100;
                }
            }
        }
    }

}
