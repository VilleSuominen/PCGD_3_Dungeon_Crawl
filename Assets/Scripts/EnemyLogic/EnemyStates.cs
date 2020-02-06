using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SA
{
    public class EnemyStates : MonoBehaviour
    {
        public bool isInvincible;
        public float health;
        public bool canMove;
        public bool isDead;
        public bool isAttacking;
        public bool hasDestination;
        public Vector3 targetDestination;

        public Animator anim;
        AnimationMove a_move;
        public Rigidbody rigid;
        public float delta;
        public NavMeshAgent agent;

        public LayerMask ignoreLayers;


        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();

        public void Init()
        {
            health = 100;
            anim = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            a_move = anim.GetComponent<AnimationMove>();
            agent = GetComponent<NavMeshAgent>();
            rigid.isKinematic = true;
            if(a_move == null)
            {
                a_move = anim.gameObject.AddComponent<AnimationMove>();
            }
            a_move.Init(null, this);
            
            InitRagdoll();
            ignoreLayers = ~(1 << 9);
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

        public void Tick()
        {
            delta = Time.deltaTime;
            canMove = anim.GetBool("canMove");
            
            if (health <= 0)
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

        public void SetDestination(Vector3 d)
        {
            if (!hasDestination)
            {
                hasDestination = true;
                agent.isStopped = false;
                agent.SetDestination(d);
                targetDestination = d;
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