using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

    public class SpawnPoint : MonoBehaviour
    {
        public GameObject Player2spawn;
        GameObject player1;
        GameObject player2;
        GameObject[] players;
        bool twoPlayers;

        private void Awake()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length >= 2)
            {
                twoPlayers = true;
                players[0].transform.position = transform.position;
                players[1].transform.position = Player2spawn.transform.position;
                player1 = players[0];
                player2 = players[1];
                Debug.Log(twoPlayers);
                Debug.Log(players.Length);
            }
            if(players.Length == 1)
            {
                player1 = players[0];
            }

        }
        // Start is called before the first frame update
        void Start()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length >= 2)
            {
                twoPlayers = true;
                players[0].transform.position = transform.position;
                players[1].transform.position = Player2spawn.transform.position;
                player1 = players[0];
                player2 = players[1];
                AddHealthP1();
                AddHealthP2();
            }
            if (players.Length == 1)
            {
                twoPlayers = false;
                players[0].transform.position = transform.position;
                player1 = players[0];
                AddHealthP1();
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (players == null)
            {
                players[0].transform.position = transform.position;
                players[1].transform.position = transform.position;
            }

        }

        public void AddHealthP1()
        {
            StateManager states1 = player1.GetComponent<StateManager>();
            if (twoPlayers)
            {
                states1.health += states1.health * 0.5f;
            }
            if (!twoPlayers)
            {
                states1.health = 100;
            }


        }

        public void AddHealthP2()
        {
            StateManager states2 = player2.GetComponent<StateManager>();
            states2.health += states2.health * 0.5f;
        }
    }
}
