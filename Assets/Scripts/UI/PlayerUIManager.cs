using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class PlayerUIManager : MonoBehaviour
    {
        GameObject player1UI;
        GameObject player1;
        GameObject player1Anchor;
        StateManager p1sm;
        StaminaController p1sc;
        Slider p1HealthBar;
        Slider p1StaminaBar;

        GameObject player2UI;
        GameObject player2;
        GameObject player2Anchor;
        StateManager p2sm;
        StaminaController p2sc;
        Slider p2HealthBar;
        Slider p2StaminaBar;

        GameObject[] players;

        public bool playerFound = false;
        public bool twoPlayers = false;

        // Start is called before the first frame update
        void Start()
        {
            player1UI = transform.Find("Player1UI").gameObject;
            player2UI = transform.Find("Player2UI").gameObject;
            p1HealthBar = transform.Find("Player1UI/HealthBar").gameObject.GetComponent<Slider>();
            p1StaminaBar = transform.Find("Player1UI/StaminaBar").gameObject.GetComponent<Slider>();
            p2HealthBar = transform.Find("Player2UI/HealthBar").gameObject.GetComponent<Slider>();
            p2StaminaBar = transform.Find("Player2UI/StaminaBar").gameObject.GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            if(twoPlayers == false || playerFound == false)
            {
                players = GameObject.FindGameObjectsWithTag("Player");

                if (playerFound == false && players.Length != 0)
                {
                    player1 = players[0];
                    player1Anchor = player1.transform.Find("UIAnchor").gameObject;
                    p1sm = player1.GetComponent<StateManager>();
                    p1sc = player1.GetComponent<StaminaController>();
                    p1HealthBar.minValue = 1;
                    p1HealthBar.maxValue = 100;
                    p1StaminaBar.minValue = 1;
                    p1StaminaBar.maxValue = p1sc.maxStamina;
                    playerFound = true;
                }

                if (twoPlayers == false && players.Length == 2)
                {
                    player2UI.gameObject.SetActive(true);
                    player2 = players[1];
                    player2Anchor = player2.transform.Find("UIAnchor").gameObject;
                    p2sm = player2.GetComponent<StateManager>();
                    p2sc = player2.GetComponent<StaminaController>();
                    p2HealthBar.minValue = 1;
                    p2HealthBar.maxValue = 100;
                    p2StaminaBar.minValue = 1;
                    p2StaminaBar.maxValue = p2sc.maxStamina;
                    twoPlayers = true;
                }

            }

            if(playerFound == true)
            {
                p1HealthBar.value = p1sm.health;
                p1StaminaBar.value = p1sc.stamina;
                Vector3 namePos = Camera.main.WorldToScreenPoint(player1Anchor.transform.position);
                player1UI.transform.position = namePos;
            }
            
            if(twoPlayers == true)
            {
                p2HealthBar.value = p2sm.health;
                p2StaminaBar.value = p2sc.stamina;
                Vector3 namePos = Camera.main.WorldToScreenPoint(player2Anchor.transform.position);
                player2UI.transform.position = namePos;
            }
        }
    }
}