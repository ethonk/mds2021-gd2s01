using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Image PHealthBar;
    private Image Hurt;
    
    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        PHealthBar = GetComponent<Image>();
        Hurt = GetComponent<Image>();
        player = FindObjectOfType <PlayerScript>();
        var tempColor = Hurt.color;
        tempColor.a = 0.0f;
        Hurt.color = tempColor;
        //Hurt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var tempColor = Hurt.color;
        tempColor.a = 1 - (player.health / 100);
        Hurt.color = tempColor;
        PHealthBar.fillAmount = player.health / player.maxHealth;
    }
}
