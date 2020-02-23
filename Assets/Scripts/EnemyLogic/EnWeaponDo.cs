﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    //this script goes to the enemy weapons!!!!
    public class EnWeaponDo : MonoBehaviour
    {
        public GameObject[] damageCollider;//add colliders to this list
        public GameObject collisionCollider;
        public GameObject shieldCollider;
        

        public void EnableDamageEnColliders()//method that activates the damage collider so damage can be done
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(true);                
            }
        }

        public void DisableDamageEnColliders()//method that deactivates the damage collider so damage isn't done all the time
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(false);
                
            }
        }

        public void EnableCollisionCollider()
        {
            collisionCollider.SetActive(true);
        }

        public void DisableCollisionCollider()
        {
            collisionCollider.SetActive(false);
        }

        //public void EnableShieldCollider()
        //{
        //    shieldCollider.SetActive(true);
        //}

        //public void DisableShieldCollider()
        //{
        //    shieldCollider.SetActive(false);
        //}

    }
}