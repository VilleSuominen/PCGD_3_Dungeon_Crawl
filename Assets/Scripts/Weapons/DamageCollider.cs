using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            EnemyStates eState = other.transform.GetComponentInParent<EnemyStates>();

            if(eState == null)
            {
                Debug.Log("null");
                return;
            }

            if (eState.aicontroller)
            {
                eState.DoDamage(20);
            }
            if (eState.aicontrollerType2)
            {
                eState.DoDamage(15);
            }
            
        }
    }
}