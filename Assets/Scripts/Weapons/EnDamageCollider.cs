﻿using System.Collections;
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
            EnemyStates eStates = transform.GetComponentInParent<EnemyStates>();

            if (states == null || other.name == "ParryCollider" || other.name == "ShieldCollider")
            {
                if(other.name == "ShieldCollider")
                {
                    //states.LookTowardsTarget();
                    states.audioController.SwordHitShieldSound();
                    states.staminaController.RemoveStamina(5f);
                    eStates.agent.isStopped = true;
                    states.rigid.AddForce(states.lookDir * 9000);
                    eStates.rigid.isKinematic = false;
                    eStates.rigid.AddForce(-eStates.rigid.transform.forward*15000);
                    
                    return;
                }
                //Debug.Log("null");
                return;
            }
            if (eStates.aicontroller != null)
            {
                states.DoDamage(25);
            }
            if(eStates.aicontrollerType2 != null)
            {
                states.DoDamage(10);
            }
            return;
            
        }
    }
}