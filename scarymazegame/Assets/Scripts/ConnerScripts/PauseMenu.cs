//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : PauseMenu
// Description : Pause script.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Panel;
    public bool m_GamePaused = false;
    MouseLook MainCamera;
    void Start()
    {
       // Panel.SetActive(false);
        m_GamePaused = false;
        MainCamera = FindObjectOfType<MouseLook>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (m_GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }



    public void Resume()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1.0f;
        m_GamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MainCamera.m_Sensitivity = 0.5f;
    }

    public void Pause()
    {
        Canvas.SetActive(true);
        Time.timeScale = 0.0f;
        m_GamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MainCamera.m_Sensitivity = 0;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!!!");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        Panel.SetActive(true);
        Canvas.SetActive(false);
    }

    public void Return()
    {
        Panel.SetActive(false);
        Canvas.SetActive(true);
    }
}
