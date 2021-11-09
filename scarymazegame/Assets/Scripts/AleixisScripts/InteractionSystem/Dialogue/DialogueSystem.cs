using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Interact
{
    public class DialogueSystem : MonoBehaviour
    {
        [Header("Audio")]
        public AudioClip BeginTalk;
        public GameObject dialogueObject;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        public bool playAudio;

        private Queue<string> sentences;

        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (playAudio)
            {
                GetComponent<AudioSource>().PlayOneShot(BeginTalk);
            }
            
            // Unlock cursor
            Cursor.lockState = CursorLockMode.None;

            // Show dialogue box
            dialogueObject.SetActive(true);

            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
        //public int MyProperty { get; set; }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }

        void EndDialogue()
        {
            // hide dialogue box
            dialogueObject.SetActive(false);

            Debug.Log("End of conversation");

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

