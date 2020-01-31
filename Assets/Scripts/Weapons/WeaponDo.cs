using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    //this script goes to the weapons!!!!
    public class WeaponDo : MonoBehaviour
    {
        public GameObject[] damageCollider;//add colliders to this list

        public void EnableDamageColliders()//method that activates the damage collider so damage can be done
        {
            for(int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(true);
            }
        }

        public void DisableDamageColliders()//method that deactivates the damage collider so damage isn't done all the time
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(false);
            }
        }

    }
}