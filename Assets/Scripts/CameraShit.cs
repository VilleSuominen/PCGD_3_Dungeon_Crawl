using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//does camera shit
public class CameraShit : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject players = GameObject.FindGameObjectWithTag("Player");
        player = players.transform;
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject players = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
            player = players.transform;
            vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = player;
        }
    }
}
