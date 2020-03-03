using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{


    public class TriggerShield : MonoBehaviour
    {
        EnemyStates eState;
        StateManager states;

        private void OnTriggerEnter(Collider other)
        {

            eState = other.transform.GetComponentInParent<EnemyStates>();
            
            StateManager otherstates = other.transform.GetComponentInParent<StateManager>();
            
            if (otherstates)
            {
                if (otherstates != GetComponentInParent<StateManager>())
                {
                    Debug.Log("player");

                    otherstates.DoDamage(100);
                }
            }
            
            if (eState.aicontrollerType2 || eState.isStunned)
            {
                Debug.Log("Enemy");

                eState.DoDamage(100);
                eState.EnableRagdoll();
                
                    
            }
            eState.Parried();
        }
    }
}