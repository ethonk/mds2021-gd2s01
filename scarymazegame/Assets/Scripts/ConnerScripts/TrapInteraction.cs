using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInteraction : MonoBehaviour
{
    public enum TrapType {Normal, Slow, Infatuation, Elemental};
    public enum TrapElement // weakness, n-1 = 1
    {
    Steel = 1, 
    Poison = 2,
    Grass = 3,
    Water = 4,
    Electric = 5,
    Rock = 6,
    Ghost = 7
    };

    [Header("Trap Type")]
    public TrapType trapType;
    public TrapElement trapElement;

    [Header("Trap Stats")]
    
    public float normalDamage;

    public float slowDamage;
    public float slowPercentage;
    public float slowTime;
    
    public float infatuationDamage;
    public float infatuationTime;

    public void OnCollisionEnter(Collision col)    // Trap and Monster interaction.
    {
        if (col.transform.gameObject.tag == "Monster")
        {
            GameObject enemy = col.transform.root.gameObject;

            switch(trapType)
            {
                case TrapType.Normal:
                    enemy.GetComponent<EnemyAI>().Debuff_Damage(normalDamage);
                    break;

                case TrapType.Slow:
                    StartCoroutine(enemy.GetComponent<EnemyAI>().Debuff_Slow(slowDamage, slowPercentage, slowTime));
                    break;

                case TrapType.Infatuation:
                    StartCoroutine(enemy.GetComponent<EnemyAI>().Debuff_Infatuation(infatuationDamage, infatuationTime));
                    break;
            }

            // Destroy trap
            Destroy(gameObject);
        }
    }
}
