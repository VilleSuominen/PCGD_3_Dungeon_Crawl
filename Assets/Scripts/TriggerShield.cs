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

            if (other.gameObject.tag == "Player")
            {
                StateManager otherstates = other.transform.GetComponentInParent<StateManager>();
                otherstates.DoDamage(100);
            }


            if (eState.isStunned || eState.aicontrollerType2)
            {
                eState.DoDamage(100);
                eState.EnableRagdoll();
            }
            eState.Parried();
        }
    }
}