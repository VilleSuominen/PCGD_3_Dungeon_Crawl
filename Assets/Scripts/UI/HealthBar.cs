using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class HealthBar : MonoBehaviour
    {

        GameObject player;
        StateManager stateManager;
        Slider bar;
        GameObject fillArea;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Controller");
            bar = GetComponent<Slider>();
            stateManager = player.GetComponent<StateManager>();
            bar.maxValue = 100;
            bar.minValue = 0;
            bar.value = bar.maxValue;
            fillArea = transform.Find("Fill Area").gameObject;
        }
            
        // Update is called once per frame
        void Update()
        {
            bar.value = stateManager.health;
            if(bar.value == bar.minValue)
            {
                fillArea.SetActive(false);
            }
        }
    }
}

