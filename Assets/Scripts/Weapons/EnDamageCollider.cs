using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class EnDamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("entered player collider");
            StateManager states = other.transform.GetComponentInParent<StateManager>();

            if (states == null || other.name == "ParryCollider")
            {
                //Debug.Log("null");
                return;
            }

            states.DoDamage(25);
        }
    }
}