using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class StaminaController : MonoBehaviour
    {
        public float stamina, maxStamina = 100, minStamina=0;
        public bool drainTime = false;

        // Start is called before the first frame update
        void Start()
        {
            stamina = 100f;
        }

        // Update is called once per frame
        void Update()
        {
            if (stamina >= maxStamina)//makes sure that stamina is never over the maxStamina
            {
                stamina = maxStamina;
            }
            if(stamina <= minStamina)//makes sure that stamina is never under the minStamina
            {
                stamina = minStamina;
            }
            if (!drainTime)
            {
                RecoverStamina();                
            }
            
        }

        //calling this method makes stamina drain over time.deltatime
        public void DrainStaminaOverTime()
        {
            if(stamina <= minStamina)
            {
                return;
            }
            stamina -= Time.deltaTime;
            drainTime = true;
        }

        //calling this method adds a chunk of stamina according to the value passed to it from the caller 
        public void AddStamina(float v)
        {
            if(stamina >= maxStamina)
            {
                return;
            }
            stamina += v;
        }

        //calling this method removes a chunk of stamina according to the value passed to it from the caller 
        public void RemoveStamina(float v)
        {
            if (stamina <= minStamina)
            {
                return;
            }
            stamina -= v;
        }
        
        //recovers stamina over time.deltatime
        public void RecoverStamina()
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime;
            }
            return;

        }

    }
}