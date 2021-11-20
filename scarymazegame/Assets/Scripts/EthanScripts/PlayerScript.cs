//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : PlayerScript.cs
// Description : Controls the values of the player.
// Author : Ethan Velasco Uy
// Mail : ethan.uy@mediadesignschool.com
//

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

    public enum CameraState {normal, inventory, shop};
    [Header("States")]
    public CameraState cameraState;

    [Header("Audio")]
    public AudioClip waterEnter;
    public AudioClip waterExit;
    public AudioClip sprintSound;
    public AudioClip pain_heavy;
    public AudioClip backpack_open;
    

    void Update()
    {
        // Update stamina bar
        staminaSlider.value = stamina/stamina_max;

        // Death check
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Win Check
        if (MonsterCount >= 7)
        {
            SceneManager.LoadScene("GameWin", LoadSceneMode.Single);
        }
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }

    public void PlayAudio(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void SwitchCameraState()
    {
        if (cameraState == CameraState.normal)
        {
            if(!(GetComponent<AudioSource>().clip == backpack_open && GetComponent<AudioSource>().isPlaying))
            {
                GetComponent<AudioSource>().PlayOneShot(backpack_open);
            }
            cameraState = CameraState.inventory;
        }
        else
        {
            cameraState = CameraState.normal;
        }
    }
}
