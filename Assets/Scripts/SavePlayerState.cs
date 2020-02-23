using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerState : MonoBehaviour
{
    private void Awake()
    {
        //Tells unity to not destroy the gameobject this script is attached to onLoad
        DontDestroyOnLoad(this.gameObject);
    }
}
