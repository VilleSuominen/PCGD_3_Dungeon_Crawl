using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {
    public class PlayerUIManager : MonoBehaviour
    {
        GameObject uiCone;
        bool blocking;
    
        // Start is called before the first frame update
        void Start()
        {
            uiCone = transform.Find("Image").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            blocking = GameObject.Find("Controller").GetComponent<StateManager>().isBlocking;
            if (blocking)
            {
                uiCone.SetActive(true);
            }
            else
            {
                uiCone.SetActive(false);
            }
        }
    }
}

