using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Values")]
    public float health = 100;
    public float maxHealth = 100;
    public float stamina = 100;
    public float stamina_max = 100;
    public float MonsterCount = 0;

    [Header("UI")]
    public Slider staminaSlider;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera inventoryCamera;

    [Header("Audio")]
    public AudioClip waterEnter;
    public AudioClip waterExit;
    public AudioClip sprintSound;
    public AudioClip pain_light;
    public AudioClip pain_heavy;
    

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }

    void Update()
    {
        // Update stamina bar
        staminaSlider.value = stamina/stamina_max;

        // Death check
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void PlayAudio(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}
