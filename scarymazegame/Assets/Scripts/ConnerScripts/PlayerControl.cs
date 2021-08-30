using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float m_Health = 100.0f;
    public bool m_inCombat = false;
    public float m_CombatTimer = 0.0f;
    public bool m_PlayerDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && m_PlayerDeath == false)
        {
            
            m_inCombat = true;
            testTakeDMG(10.0f);
            m_CombatTimer = 3.0f;
        }
        //else 
        //{
        //    m_PlayerDeath = true;
        //}
        if (m_Health <= 0.0f)
        {
            m_PlayerDeath = true;
            m_Health = 0.0f;
        }
        if (m_inCombat)
        {
            m_CombatTimer -= Time.deltaTime;
            if(m_CombatTimer <= 0.0f)
            {
                m_inCombat = false;
                m_CombatTimer = 0.0f;
            }
        }
        if (m_inCombat == false)
        {
            if (m_Health < 100.0f)
            {
                m_Health += Time.deltaTime * 10;
            }
            else if (m_Health > 100.0f)
            {
                m_Health = 100.0f;
            }
            if (m_PlayerDeath)
            {

            }
        }
    }
    void testTakeDMG(float DMG)
    {
        m_Health -= DMG;
    }
}
