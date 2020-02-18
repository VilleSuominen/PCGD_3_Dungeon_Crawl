using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class TriggerShield : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("entered shield collider");
            EnemyStates eState = other.transform.GetComponentInParent<EnemyStates>();
            StateManager states = GetComponentInParent<StateManager>();

            if (eState == null)
            {
                Debug.Log("null");
                states.DoDamage(100);
                return;
            }
            
            Debug.Log("ragdoll enabled");
            eState.DoDamage(100);
            eState.EnableRagdoll();
        }
    }
}