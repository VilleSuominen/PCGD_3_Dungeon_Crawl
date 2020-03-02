using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA {
    public class SavePlayerState : MonoBehaviour
    {
        [HideInInspector]
        public StateManager states;


        private void Awake()
        {
            //Tells unity to not destroy the gameobject this script is attached to onLoad
            states = GetComponent<StateManager>();
            if (!states.isDead)
            {
                DontDestroyOnLoad(gameObject);
            }
                
        }

        private void Start()
        {
            if (!states.isDead)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}