using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Image PHealthBar;
    private Image Hurt;
    
    public float m_PCurrentHealth;
    private const float m_PMaxHealth = 100.0f;
    PlayerControl Player;
    // Start is called before the first frame update
    void Start()
    {
        PHealthBar = GetComponent<Image>();
        Hurt = GetComponent<Image>();
        Player = FindObjectOfType < PlayerControl>();
        var tempColor = Hurt.color;
        tempColor.a = 0.0f;
        Hurt.color = tempColor;
        //Hurt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var tempColor = Hurt.color;
        tempColor.a = 1 - (m_PCurrentHealth / 100);
        Hurt.color = tempColor;
        m_PCurrentHealth = Player.m_Health;
        PHealthBar.fillAmount = m_PCurrentHealth / m_PMaxHealth;


    }


}
