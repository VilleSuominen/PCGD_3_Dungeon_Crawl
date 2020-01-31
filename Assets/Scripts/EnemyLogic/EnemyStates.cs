using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class EnemyStates : MonoBehaviour
    {
        public bool isInvincible;
        public float health;
        Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if(isInvincible)
            isInvincible = !anim.GetBool("canMove");
        }

        public void DoDamage(float v)
        {
            if (isInvincible)
            {
                return;
            }

            health -= v;
            isInvincible = true;
            anim.Play("damage animation here");
        }


    }
}