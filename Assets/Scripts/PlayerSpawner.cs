using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//spawns the player on the location where this scripts parent gameobject resides
public class PlayerSpawner : MonoBehaviour
{
    //GameObject player;
    public PlayerInputManager manager;
    public GameObject playerPrefab { get; set; }

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.position = transform.position;
        manager.playerPrefab.transform.position = transform.position;
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
