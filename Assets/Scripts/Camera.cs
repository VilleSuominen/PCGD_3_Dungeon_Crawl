using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//does camera shit
public class Camera : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    GameObject players;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player");
        if(players == null)
        {
            return;
        }
        player = players.transform;
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (players == null)
        {
            players = GameObject.FindGameObjectWithTag("Player");
            if(players == null)
            {
                return;
            }
            if (players.CompareTag("DeadPlayer"))
            {
                players = GameObject.FindGameObjectWithTag("Player");
                player = players.transform;
                vcam = GetComponent<CinemachineVirtualCamera>();
                vcam.Follow = player;

            }
            player = players.transform;
            vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = player;

        }
        if (players.CompareTag("DeadPlayer"))
        {
            players = GameObject.FindGameObjectWithTag("Player");
            if (players == null)
            {
                return;
            }
            player = players.transform;
            vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = player;

        }
    }
}
