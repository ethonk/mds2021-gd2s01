//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : Trapinteraction
// Description : The interactions of all the traps.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInteraction : MonoBehaviour
{
    public enum TrapType {Normal, Slow, Infatuation, Elemental};
    public enum TrapElement {Steel, Poison,  Grass, Water, Electric, Rock, Ghost};

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
                    enemy.GetComponent<EnemyAI>().Debuff_Infatuation(infatuationDamage, infatuationTime);
                    break;
            }

            // Destroy trap
            Destroy(gameObject);
        }
    }
}
