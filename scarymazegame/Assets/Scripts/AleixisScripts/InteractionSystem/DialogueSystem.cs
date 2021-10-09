using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Interact
{
    public class DialogueSystem : MonoBehaviour
    {

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;

        private Queue<string> sentences;

        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
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

            Debug.Log("End of conversation");
        }
    }
}

