using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SA
{
    public class EnemyStates : MonoBehaviour
    {
        [Header("EnemyStates")]
        public bool isInvincible;
        public float health;
        public bool canMove;
        public bool isDead;
        public bool isAttacking;
        public bool canBeParried = false;
        public bool isStunned;
        public bool takesDamage = true;
        public bool hitEnemy;        

        [Header("NavMeshAgent")]
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
        public AIControllerType2 aicontrollerType2;


        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();

        //called on start, what components and states are initialized
        public void Init()
        {
            health = 100;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            states = player.GetComponent<StateManager>();
            anim = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            a_move = anim.GetComponent<AnimationMove>();
            agent = GetComponent<NavMeshAgent>();
            aicontroller = GetComponent<AIController>();
            if (aicontroller == null)
            {
                aicontrollerType2 = GetComponent<AIControllerType2>();
            }            
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

        //initializes ragdoll
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

        //enables ragdoll duh
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

        //closes the animator so shit don't go crazy after enemy is killed
        IEnumerator CloseAnimator()
        {
            yield return new WaitForEndOfFrame();
            anim.enabled = false;
            this.enabled = false;
        }

        //this method is called on the update
        public void Tick()
        {
            if (states == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                states = player.GetComponent<StateManager>();
            }

            delta = Time.deltaTime;
            canMove = anim.GetBool("canMove");
                        
            if (!states.isBlocking)
            {
                rigid.isKinematic = true;
            }
            //too hard
            //if (health < 75)
            //{
            //    agent.speed = 2f;                
            //}
            if (health <= 0)
            {
                if (!isDead)
                {                    
                    isDead = true;
                    EnableRagdoll();
                    if (aicontroller)
                    {
                        states.audioController.EnemyDeathSound();
                    }
                    if (aicontrollerType2)
                    {
                        states.audioController.GoblinDeath();
                    }
                }
            }

            if (isInvincible)
            {
                isInvincible = !canMove;                
            }

            AIControllerSwitcher();

            if(canMove == false)
            {
                anim.applyRootMotion = false;                
            }
            
        }

        //checks which type of enemy aicontroller is being used
        void AIControllerSwitcher()
        {
            if(aicontroller != null)
            {
                if (canMove == true && aicontroller.angle <= 90)
                {
                    takesDamage = false;
                }
                if (aicontroller.angle > 90)
                {
                    takesDamage = true;
                }
            }
            if(aicontroller == null)
            {
                takesDamage = true;
            }
        }

        //sets destination for the navmesh agent
        public void SetDestination(Vector3 d)
        {
            if (!hasDestination && canMove)
            {
                anim.SetFloat("walk_speed", agent.speed*1.5f);
                anim.SetFloat("vertical", 1f);
                hasDestination = true;
                agent.isStopped = false;
                agent.SetDestination(d);
                targetDestination = d;
            }            
        }
        
        //This method checks if the enemy is in a state where it can be damaged
        public void DoDamage(float v)
        {
            
            hitEnemy = true;
            if (!takesDamage)
            {                
                rigid.isKinematic = false;
                return;
            }
            if (aicontrollerType2)
            {
                Debug.Log("ShouldTakeDamage: " + takesDamage);
                states.audioController.GoblinHit();
                health -= v;
                isInvincible = true;
                Debug.Log("diddamage" + health);
                //anim.Play("Damage");
                anim.applyRootMotion = true;
            }
            if (aicontroller)
            {
                Debug.Log("ShouldTakeDamage: " + takesDamage);
                states.audioController.SwordHitSkellySound();
                health -= v;
                isInvincible = true;
                Debug.Log("diddamage" + health);
                //anim.Play("Damage");
                anim.applyRootMotion = true;
            }
            
        }

        //Parry method, these things happen when enemy is parried
        public void Parried()
        {
            //isInvincible = true;
            states.audioController.Parry();
            states.staminaController.RemoveStamina(15f);
            a_move.DisableEnDamageColliders();
            Debug.Log("EnemyStunned");
            takesDamage = true;
            anim.Play("Stun");
            anim.applyRootMotion = true;
            anim.SetBool("canMove", false);
        }

    }
}