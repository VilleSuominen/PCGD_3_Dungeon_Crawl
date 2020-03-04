using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA {
    public class SavePlayerState : MonoBehaviour
    {
        [HideInInspector]
        public StateManager states;
        bool save;
        

        private void Awake()
        {
            //Tells unity to not destroy the gameobject this script is attached to onLoad
            states = GetComponent<StateManager>();
            if (states.isDead)
            {
                
                Destroy(this.gameObject);
            }
            if (!states.isDead)
            {
                
                DontDestroyOnLoad(gameObject);
            }
                
        }
        private void Update()
        {
            
            if (states.isDead)
            {
                StartCoroutine(WaitUntilDestroy());
                
            }
            if (!states.isDead)
            {
                
                DontDestroyOnLoad(gameObject);

            }
        }

        IEnumerator WaitUntilDestroy()
        {

            yield return new WaitForSeconds(7);
            Destroy(this.gameObject);
        }


    }
}