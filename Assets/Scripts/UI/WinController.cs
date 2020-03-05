using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SA
{
    public class WinController : MonoBehaviour
    {
        Text genText;
        public Font font;

        void Awake()
        {
            genText = GameObject.Find("GameUI/GeneralText").GetComponent<Text>();
        }

        private void OnTriggerEnter(Collider other)
        {
            genText.font = font;
            genText.color = Color.black;
            genText.text = "You've Won!";
            StartCoroutine("Reset");
        }
        
        IEnumerator Reset()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(0);
        }
    }
}