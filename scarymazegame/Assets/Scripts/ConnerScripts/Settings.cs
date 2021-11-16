//
// Bachelor of Software Engineering
// Media Design School
// Auckland
// New Zealand
//
// (c) 2020 Media Design School
//
// File Name : Settings
// Description : The main function for Battleships.
// Author : Conner Hall
// Mail : conner.hall@mediadesignschool.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    bool m_IsFullScreen = true;
    public Dropdown ResDropDown;

    public AudioMixer audioMixer;

    Resolution[] m_resolutions;
    
    void Start()
    {
        m_resolutions = Screen.resolutions;

        ResDropDown.ClearOptions();

        List<string> Options = new List<string>();

        int m_CurrentResIndex = 0;
        
        for (int i = 0; i < m_resolutions.Length; i++)
        {
            string Option = m_resolutions[i].width + " x " + m_resolutions[i].height;
            Options.Add(Option);

            if (m_resolutions[i].width == Screen.currentResolution.width && m_resolutions[i].height == Screen.currentResolution.height)
            {
                m_CurrentResIndex = i;
            }

        }

        ResDropDown.AddOptions(Options);
        ResDropDown.value = m_CurrentResIndex;
        ResDropDown.RefreshShownValue();
    }


   public void SetVolume (float m_fVolume)
    {
        audioMixer.SetFloat("AVolume", m_fVolume);
    }


    public void SetQuality (int m_QualityIndex)
    {
        QualitySettings.SetQualityLevel(m_QualityIndex);
        Debug.Log(m_QualityIndex);
    }

    public void SetRes(int ResIndex)
    {
        Resolution m_Res = m_resolutions[ResIndex];
        Screen.SetResolution(m_Res.width, m_Res.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool m_IsFullScreen)
    {
        Screen.fullScreen = m_IsFullScreen;
    }
}
