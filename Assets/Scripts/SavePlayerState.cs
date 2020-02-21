using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerState : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
