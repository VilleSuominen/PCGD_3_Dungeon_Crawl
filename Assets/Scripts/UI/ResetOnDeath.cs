using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SA
{
    public class ResetOnDeath : MonoBehaviour
    {
        string text;
        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<Text>().text;
            player = GameObject.Find("Controller");
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null && player.GetComponent<StateManager>().isDead == true)
            {
                StartCoroutine("Reset");
            }
        }

        IEnumerator Reset()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(0);
        }
    }
}