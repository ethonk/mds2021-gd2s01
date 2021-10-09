using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Interact
{
    public class InteractionUI : MonoBehaviour
    {
       [SerializeField] private Slider progressBar;
       [SerializeField] private TextMeshProUGUI textTooltip;

        public void SetTooltip(string tooltip) // sets tool tip to whatever
        {
            textTooltip.SetText(tooltip);
        }

        public void UpdateProgressBar(float ValueAmount) // updates progress bar with a given amount
        {
            progressBar.value = ValueAmount;
        }

        public void ResetUI() // called when UI needs to be reset
        {
            progressBar.value = 0f; // progress bar value is null
            textTooltip.SetText(""); // sets text to null
        }

        public void HideBar() // custom function made to hide the bar, I think I didn't need to do this because I did a fix but I just keep it like this, its simple
        {
            progressBar.gameObject.SetActive(false);
        }

        public void ShowBar() // same thing but shows the bar
        {
            progressBar.gameObject.SetActive(true);
        }
        
    }
}


