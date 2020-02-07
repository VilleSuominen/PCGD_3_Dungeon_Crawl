using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA {
    public class ParryCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnemyStates eSt = other.transform.GetComponentInParent<EnemyStates>();
            Debug.Log("entered parryC");
            if (eSt == null || eSt.canBeParried == false)
            {
                return;
            }
            
            eSt.Parried();

            
        }
    }
}