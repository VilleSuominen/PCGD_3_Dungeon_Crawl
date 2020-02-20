using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class TriggerShield : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            EnemyStates eState = other.transform.GetComponentInParent<EnemyStates>();
            StateManager states = GetComponentInParent<StateManager>();

            if (eState == null)
            {
                
                states.DoDamage(100);
                return;
            }
                        
            eState.DoDamage(100);
            eState.EnableRagdoll();
        }
    }
}