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
        GameObject[] players;
        bool setup1Done;
        bool setup2Done;
        float p1health;
        float p2health;
        GameObject ui;
        PlayerUIManager playerUIManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 1.0f)
            {
                if (setup1Done == false)
                {
                    ui = GameObject.Find("GameUI/PlayerUIs");

                    if (ui != null && ui.GetComponent<PlayerUIManager>().playerFound == true)
                    {
                        playerUIManager = ui.GetComponent<PlayerUIManager>();
                        p1health = ui.transform.Find("Player1UI/HealthBar").GetComponent<Slider>().value;

                        if (p1health != 0)
                        {
                            setup1Done = true;
                        }
                    }
                }

                else
                {
                    if (playerUIManager.twoPlayers == true && setup2Done == false)
                    {
                        p2health = ui.transform.Find("Player2UI/HealthBar").GetComponent<Slider>().value;

                        if (p2health != 0)
                        {
                            setup2Done = true;
                        }
                    }

                    if (playerUIManager.twoPlayers == false && p1health <= 0)
                    {
                        GetComponent<Text>().text = "YOU DIED";
                        StartCoroutine("Reset");
                    }

                    else if (playerUIManager.twoPlayers == true && p1health <= 0 && p2health <= 0)
                    {
                        GetComponent<Text>().text = "YOU DIED";
                        StartCoroutine("Reset");
                    }
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