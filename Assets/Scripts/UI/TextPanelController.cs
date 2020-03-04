using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class TextPanelController : MonoBehaviour
    {
        public float delay = 0.1f;
        public string fullText;
        private string currentText;
        bool isTyping = false;
        bool endReached = false;

        Text activeTextBox;
        Button nextButton;

        void OnEnable()
        {
            nextButton = transform.Find("NextButton").gameObject.GetComponent<Button>();
            activeTextBox = transform.Find("TextPanel/TextBox1").gameObject.GetComponent<Text>();
            fullText = activeTextBox.text;
            StartCoroutine("ShowText");
        }

        IEnumerator ShowText()      // Typewriting effect for the textbox
        {
            isTyping = true;
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                activeTextBox.text = currentText;
                yield return new WaitForSecondsRealtime(delay);
            }
            isTyping = false;
        }

        public void NextButton()         // Called by clicking the Next-button
        {
            if (isTyping == true)       // Skips to the end of the typewriting effect
            {
                StopCoroutine("ShowText");
                activeTextBox.text = fullText;
                isTyping = false;
            }

            else if (endReached == true)     // Closes the panel
            { 
                GameObject.Find("GameUI").GetComponent<MenuController>().Resume();
                gameObject.SetActive(false);
            }

           
            else if (isTyping == false && endReached == false)   // Opens the second page
            {
                transform.Find("TextPanel/TextBox1").gameObject.SetActive(false);
                transform.Find("TextPanel/TextBox2").gameObject.SetActive(true);
                activeTextBox = transform.Find("TextPanel/TextBox2").gameObject.GetComponent<Text>();
                fullText = activeTextBox.text;
                StartCoroutine("ShowText");
                endReached = true;
            }
        }
    }
}
