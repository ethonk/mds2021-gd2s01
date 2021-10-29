using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingScript : MonoBehaviour
{
    public MonsterDetails EnemyDetails;
    public GameObject Player;

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
            if(Enemy.GetComponent<MonsterDetails>().health > 50)
            {
                Player.GetComponent<PlayerScript>().MonsterCount += 1;
                Destroy(Enemy);
            }
            else 
            {
                Enemy.GetComponent<MonsterDetails>().health += 50;
            }
        }
    }

}
