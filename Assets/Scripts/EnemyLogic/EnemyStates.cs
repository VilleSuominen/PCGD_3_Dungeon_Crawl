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
        public bool canBeParried = false;
        public bool isStunned;
        public bool takesDamage = true;
        public bool hitEnemy;
        public float walkAnimSpeed;

        public bool hasDestination;
        public Vector3 targetDestination;

        public Animator anim;
        AnimationMove a_move;
        public Rigidbody rigid;
        public float delta;
        public NavMeshAgent agent;
        public StateManager states;
        [HideInInspector]
        public EnWeaponManager enemyWeaponManager;
        public LayerMask ignoreLayers;
        public AIController aicontroller;


        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();

        public void Init()
        {
            health = 100;
            anim = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            a_move = anim.GetComponent<AnimationMove>();
            agent = GetComponent<NavMeshAgent>();
            aicontroller = GetComponent<AIController>();
            rigid.isKinematic = true;
            enemyWeaponManager = GetComponent<EnWeaponManager>();
            enemyWeaponManager.Init();
            if(a_move == null)
            {
                a_move = anim.gameObject.AddComponent<AnimationMove>();
            }
            a_move.Init(null, this);
            //a_move.EnableEnShieldCollider();
            InitRagdoll();
            ignoreLayers = ~(0 << 10);
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
            //Debug.Log(canBeParried);
            if (states.isBlocking && !isDead)
            {
                rigid.isKinematic = false;
            }
            if (!states.isBlocking)
            {
                rigid.isKinematic = true;
            }
            if (health <= 0)
            {
                if (!isDead)
                {
                    
                    isDead = true;
                    EnableRagdoll();
                    states.audioController.EnemyDeathSound();
                }
            }

            if (isInvincible)
            {
                isInvincible = !canMove;
                
            }
            if(canMove == true && aicontroller.angle <= 90)
            {                
                takesDamage = false;
            }
            if(aicontroller.angle > 90)
            {
                takesDamage = true;
            }
            if(canMove == false)
            {
                anim.applyRootMotion = false;
                
            }
            
        }

        public void SetDestination(Vector3 d)
        {
            if (!hasDestination && canMove)
            {
                anim.SetFloat("walk_speed", walkAnimSpeed);
                anim.SetFloat("vertical", 1f);
                hasDestination = true;
                agent.isStopped = false;
                agent.SetDestination(d);
                targetDestination = d;
            }
            
        }
        void BackDamage()
        {
            if (aicontroller.angle < 90 || canMove)
            {
                takesDamage = false;
            }
            else
            {
                takesDamage = true;
            }
            
            
        }
        public void DoDamage(float v)
        {
            //if (isInvincible)
            //{
            //    Debug.Log("isinvincible");
            //    return;
            //}
            hitEnemy = true;
            if (!takesDamage)
            {
                states.audioController.SwordHitShieldSound();
                rigid.isKinematic = false;
                //rigid.AddForce(-targetDestination * 900);
                Debug.Log("ShouldTakeDamage: "+ takesDamage);
                return;
            }
            Debug.Log("ShouldTakeDamage: " + takesDamage);
            states.audioController.SwordHitSkellySound();
            health -= v;
            isInvincible = true;
            Debug.Log("diddamage"+health);
            //anim.Play("Damage");
            anim.applyRootMotion = true;
        }

        public void Parried()
        {

            //isInvincible = true;
            states.audioController.Parry();
            //states.staminaController.AddStamina(25f);
            a_move.DisableEnDamageColliders();
            Debug.Log("EnemyStunned");
            takesDamage = true;
            anim.Play("Stun");
            anim.applyRootMotion = true;
            anim.SetBool("canMove", false);

        }

    }
}