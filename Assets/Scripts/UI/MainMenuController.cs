using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class MainMenuController : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.activeSelf == true)
            {
                Time.timeScale = 0.0f;
            }
        }

        public void Resume()
        {
            Time.timeScale = 1.0f;
        }


    }
}
