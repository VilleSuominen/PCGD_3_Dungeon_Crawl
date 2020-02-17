using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class StaminaBar : MonoBehaviour
    {

        GameObject player;
        StaminaController staminaController;
        Slider bar;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Controller");
            bar = GetComponent<Slider>();
            staminaController = player.GetComponent<StaminaController>();
            bar.minValue = staminaController.minStamina;
            bar.maxValue = staminaController.maxStamina;
        }

       // Update is called once per frame
        void Update()
        {
            bar.value = staminaController.stamina;
        }
    }
}

