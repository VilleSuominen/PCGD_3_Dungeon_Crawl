using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SA
{
    //spawns the player on the location where this scripts parent gameobject resides
    public class PlayerSpawner : MonoBehaviour
    {
        public PlayerInputManager manager;
        public GameObject playerPrefab { get; set; }
        public GameObject Player2spawn;
        GameObject player1;
        GameObject player2;
        GameObject[] players;
        bool twoPlayers;

        private void Awake()
        {
            manager = GetComponent<PlayerInputManager>();
            manager.playerPrefab.transform.position = transform.position;
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
            if (players.Length == 1)
            {
                twoPlayers = false;
                player1 = players[0];
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnPlayerJoined()
        {

            Debug.Log(manager.playerPrefab.transform.position);

        }
    }
}