using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SA
{
    public class ResetOnDeath : MonoBehaviour
    {
        GameObject player;
        bool death = false;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Controller");
        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
            {
                player = GameObject.Find("Controller");
                if (player == null)
                {
                    return;
                }
                
            }
            if (death == false)
            {
                if (player == null && player.GetComponent<StateManager>().isDead == true)
                {
                    death = true;
                    GetComponent<Text>().text = "YOU DIED";
                    StartCoroutine("Reset");
                }
            }
        }

        IEnumerator Reset()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(0);
        }
    }
}