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
                return;
            }

            eState.DoDamage(5);
        }
    }
}