using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class EnemyStates : MonoBehaviour
    {
        public bool isInvincible;
        public float health;
        public bool canMove;
        public bool isDead;

        public Animator anim;
        AnimationMove a_move;
        public Rigidbody rigid;
        public float delta;

        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();

        private void Start()
        {
            health = 100;
            anim = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            a_move = anim.GetComponent<AnimationMove>();
            if(a_move == null)
            {
                a_move = anim.gameObject.AddComponent<AnimationMove>();
            }
            a_move.Init(null, this);

            InitRagdoll();
            
        }

        void InitRagdoll()
        {
            Rigidbody [] rigs = GetComponentsInChildren<Rigidbody>();
            for(int i = 0; i<rigs.Length; i++)
            {
                if(rigs[i] == rigid)
                {
                    continue;
                }
                ragdollRigids.Add(rigs[i]);
                rigs[i].isKinematic = true;
                Collider col = rigs[i].GetComponent<Collider>();
                col.isTrigger = true;
                ragdollColliders.Add(col);
            }
        }

        public void EnableRagdoll()
        {
            
            for(int i = 0; i<ragdollRigids.Count; i++)
            {
                ragdollRigids[i].isKinematic = false;
                ragdollColliders[i].isTrigger = false;
            }
            Collider controllerCollider = rigid.gameObject.GetComponent<Collider>();
            controllerCollider.enabled = false;
            rigid.isKinematic = true;
            StartCoroutine("CloseAnimator");
        }

        IEnumerator CloseAnimator()
        {
            yield return new WaitForEndOfFrame();
            anim.enabled = false;
            this.enabled = false;
        }

        private void Update()
        {
            delta = Time.deltaTime;
            canMove = anim.GetBool("canMove");

            if(health <= 0)
            {
                if (!isDead)
                {
                    isDead = true;
                    EnableRagdoll();
                }
            }

            if (isInvincible)
            {
                isInvincible = !canMove;
                
            }

            if(canMove == false)
            {
                anim.applyRootMotion = false;
            }
            
        }

        public void DoDamage(float v)
        {
            //if (isInvincible)
            //{
            //    Debug.Log("isinvincible");
            //    return;
            //}

            health -= v;
            isInvincible = true;
            Debug.Log("diddamage"+health);
            //anim.Play("Damage");
            anim.applyRootMotion = true;
        }


    }
}