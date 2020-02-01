using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("entered collider");
            EnemyStates eState = other.transform.GetComponentInParent<EnemyStates>();

            if(eState == null)
            {
                Debug.Log("null");
                return;
            }

            eState.DoDamage(10);
        }
    }
}