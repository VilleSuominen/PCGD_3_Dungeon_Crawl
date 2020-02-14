using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{


    public class AnimationMove : MonoBehaviour
    {
        Animator anim;
        StateManager states;
        Rigidbody rigid;
        EnemyStates eStates;
        float d;

        public void Init(StateManager st, EnemyStates eSt)
        {
            states = st;
            eStates = eSt;
            //anim = st.anim;            
            if(st != null)
            {
                anim = st.anim;
                rigid = st.rigid;
                d = st.delta;
            }
            if(eSt != null)
            {
                anim = eSt.anim;
                rigid = eSt.rigid;
                d = eSt.delta;

            }
        }

        void OnAnimatorMove()
        {
            if(states == null && eStates == null)
            {
                return;
            }
            if(states != null)
            {
                return;
            }
            if(rigid == null)
            {
                return;
            }

            if (states !=null)
            {
                if(states.canMove)
                {
                    return;
                }
                d = states.delta;
            }
            if(eStates != null)
            {
                if (eStates.canMove)
                {
                    return;                    
                }
                d = eStates.delta;
            }

            rigid.drag = 0;
            float multiplier = 1;

            Vector3 delta = anim.deltaPosition;
            delta.y = 0;
            Vector3 v = (delta * multiplier) / d;
            rigid.velocity = v;
        }
        //these are triggered through the animation event system
        public void EnableDamageColliders()
        {
            if(states == null)
            {
                return;
            }
            states.weaponManager.currentWeapon.wDo.EnableDamageColliders();
            

        }

        public void DisableDamageColliders()
        {
            if(states == null)
            {
                return;
            }
            states.weaponManager.currentWeapon.wDo.DisableDamageColliders();
            
        }

        public void EnableEnDamageColliders()
        {
            if (eStates == null)
            {
                return;
            }
            eStates.enemyWeaponManager.enemyWeapon.ewDo.EnableDamageEnColliders();
            eStates.canBeParried = true;
        }

        public void DisableEnDamageColliders()
        {
            if (eStates == null)
            {
                return;
            }
            eStates.enemyWeaponManager.enemyWeapon.ewDo.DisableDamageEnColliders();
            eStates.canBeParried = false;
        }

        public void EnableParryCollider()
        {
            if(states == null)
            {
                return;
            }
            states.weaponManager.currentWeapon.wDo.EnableParryCollider();
        }

        public void DisableParryCollider()
        {
            if(states == null)
            {
                return;
            }
            states.weaponManager.currentWeapon.wDo.DisableParryCollider();
        }

        public void RemoveStamina()
        {
            if(states == null)
            {
                return;
            }
            states.staminaController.RemoveStamina(25);
        }
    }
}