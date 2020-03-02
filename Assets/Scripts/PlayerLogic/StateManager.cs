using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    //players movement/animation states happen in this class
    public class StateManager : MonoBehaviour
    {
        public GameObject activeModel;

        [Header("Inputs")]
        public Vector2 movement;
        public Vector2 rotation;
        public float v_rot;
        public float h_rot;
        public Vector3 lookDir;
        public bool block, backstep, attack, charge;
        public Vector3 moveDir;
        public float moveSpeed = 2.5f;
        public float rotateSpeed = .02f;
        public float moveAmount;
        public float toGround = .5f;
        public float health;        
        
        [Header("States")]
        public bool onGround;
        public bool inAction;
        public bool canMove;
        public bool isTwoHanded;
        public bool isBlocking;
        public bool isDead;
        public bool lockOn;

        GameObject[] enemy;        
        public Transform lockOnTarget;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public AnimationMove a_move;
        [HideInInspector]
        public ActionManager a_man;
        [HideInInspector]
        public WeaponManager weaponManager;
        [HideInInspector]
        public StaminaController staminaController;
        [HideInInspector]
        public AudioController audioController;
        [HideInInspector]
        public MouseTurning mouseTurning;
        Transform mTransform;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();

        float _delay;


        void InitRagdoll()
        {
            Rigidbody[] rigs = GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < rigs.Length; i++)
            {
                if (rigs[i] == rigid)
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

            for (int i = 0; i < ragdollRigids.Count; i++)
            {
                ragdollRigids[i].isKinematic = false;
                ragdollColliders[i].isTrigger = false;
            }
            Collider controllerCollider = rigid.gameObject.GetComponent<Collider>();
            controllerCollider.enabled = false;
            rigid.isKinematic = true;
            StartCoroutine("CloseAnimator");
        }

        public void Init()
        {            
            SetupAnimator();
            
            rigid = GetComponent<Rigidbody>();
            //rigid.isKinematic = true;
            mTransform = this.transform;
            rigid.angularDrag = 999;
            rigid.drag = 4;
            health = 100;
            mouseTurning = GetComponent<MouseTurning>();
            weaponManager = GetComponent<WeaponManager>();
            weaponManager.Init();
            staminaController = GetComponent<StaminaController>();
            audioController = GetComponent<AudioController>();            
            a_man = GetComponent<ActionManager>();
            a_man.Init(this);

            a_move = activeModel.GetComponent<AnimationMove>();
            if(a_move == null)
            {
                a_move = activeModel.AddComponent<AnimationMove>();
            }           
            a_move.Init(this, null);
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            
            if(enemy != null)
            {                
                lockOnTarget = NearestEnemy();
                Debug.Log(lockOnTarget);
            }
            InitRagdoll();
            gameObject.layer = 8;
            mouseTurning.enabled = false;
            ignoreLayers = ~(1 << 9);
        }

        Transform NearestEnemy()
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (GameObject ene in enemy)
            {
                Transform potentialTarget = ene.transform;
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
            
        }

        //sets up the animator for the current model in use
        void SetupAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.Log("No model");
                }
                else
                {
                    activeModel = anim.gameObject;
                }
            }
            if (anim == null)
            {
                anim = activeModel.GetComponent<Animator>();
            }
            anim.applyRootMotion = false;
        }

        //handles updating and checking states in fixedupdate
        public void FixedTick(float d)
        {
            delta = d;

            isBlocking = false;
            if(enemy == null)
            {
                enemy = GameObject.FindGameObjectsWithTag("Enemy");
                lockOnTarget = NearestEnemy();
                Debug.Log(lockOnTarget);
            }
            
            CheckAction();
            anim.SetBool("block", isBlocking);            
            if (inAction)
            {
                anim.applyRootMotion = true;
                
                _delay += delta;
                if(_delay > 0.3f)
                {
                    inAction = false;
                    _delay = 0;
                }
                else
                {
                    return;
                }                              
            }

            canMove = anim.GetBool("canMove");
            //canMove = true;
            
            if (!canMove)
            {
                return;
            }
            anim.applyRootMotion = false;

            rigid.drag = (moveAmount > 0 || onGround == false) ? 0 : 4;
            anim.SetFloat("a_speed", moveSpeed);
            
            if (onGround)
            {                
                if(isBlocking == true)
                {                    
                    moveSpeed = 1f;                    
                    weaponManager.currentWeapon.wDo.EnableShieldCollider();
                    staminaController.DrainStaminaOverTime();                    
                }
                else
                {                    
                    weaponManager.currentWeapon.wDo.DisableShieldCollider();
                    moveSpeed = 2.5f;                    
                    staminaController.drainTime = false;
                }
                rigid.velocity = moveDir * (moveSpeed * moveAmount);
            }
            //if (Input.GetMouseButtonDown(2))
            //{
            //    mouseTurning.enabled = true;
            //    Debug.Log(mouseTurning.enabled);
            //}
            //if (Input.GetButtonDown("activatePad"))
            //{
            //    mouseTurning.enabled = false;
            //    Debug.Log(mouseTurning.enabled);
            //}
            
            Vector3 lookDirection = lookDir;
            
            if (lockOn && lockOnTarget != null)
            {
                
                LookTowardsTarget();
            }

            if (lookDir == Vector3.zero)
            {
                lookDir = transform.forward;
            }
            if (mouseTurning.enabled)
            {
                Cursor.visible = true;
                if (!lockOn)
                {
                    mouseTurning.Turning();
                }
                
            }
            if(!mouseTurning.enabled)
            {
                Cursor.visible = true;
                Quaternion lookRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, delta / rotateSpeed);
            }
            MovementAnimationHandler();
        }

        //checks what action/animation can be done according to given conditions
        public void CheckAction()
        {
            if(canMove == false)
            {
                return;
            }

            if(block == false && backstep == false && attack == false && charge == false)
            {
                return;
            }

            

            Action slot = a_man.GetActionSlot(this);
            if(slot == null)
            {
                return;
            }
            switch (slot.type)
            {
                case ActionTypes.attack:
                    AttackAction(slot);
                    break;
                case ActionTypes.block:
                    BlockAction(slot);
                    break;
            }
            
            
        }

        //handles what attacks are used
        void AttackAction(Action slot)
        {
            string targetAnim = null;
            targetAnim = slot.targetAnim;
            
            if (string.IsNullOrEmpty(targetAnim)||staminaController.stamina<15f)
            {
                return;
            }
            if(targetAnim == "walk")
            {
                if (staminaController.stamina > 25f)
                {
                    BackStep();
                }
                else
                {
                    return;
                }
                
            }

            if(targetAnim == "shield")
            {
                if (staminaController.stamina >= 75f)
                {
                    ShieldChargeAttack();
                }
                else
                {
                    return;
                }
                
            }        
            
            canMove = false;
            inAction = true;
            
            anim.CrossFade(targetAnim, 0.3f);
            //rigid.velocity = Vector3.zero;
        }

        //blocking
        void BlockAction(Action slot)
        {      
            if(staminaController.stamina < 5)
            {
                return;
            }
            isBlocking = true;
        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
            if (health <= 0)
            {
                if (!isDead)
                {
                    isDead = true;
                    audioController.PlayerDeath();
                    GameObject.Find("GameUI/GeneralText").GetComponent<Text>().text = "YOU DIED";
                    EnableRagdoll();
                    Collider controllerCollider = rigid.gameObject.GetComponent<Collider>();
                    controllerCollider.enabled = false;
                    InputHandlerAlpha input = rigid.gameObject.GetComponent<InputHandlerAlpha>();
                    input.enabled = false;
                    rigid.gameObject.tag = "DeadPlayer";
                    rigid.isKinematic = true;
                    StartCoroutine("CloseAnimator");
                    
                }
            }
        }

        IEnumerator CloseAnimator()
        {
            yield return new WaitForEndOfFrame();
            anim.enabled = false;
            this.enabled = false;
        }

        //plays movement animations
        void MovementAnimationHandler()
        {
            anim.SetFloat("vertical", moveAmount, 0.2f, delta);

        }
        
        //unused
        public void BackStep()
        {
            staminaController.RemoveStamina(25);
            moveSpeed = 5;
            anim.SetFloat("a_speed", moveSpeed*2);
            Quaternion lookrotation = Quaternion.LookRotation(-moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, delta / rotateSpeed);
            rigid.velocity = moveDir * (moveSpeed * 2);
        }
        
        //unused
        public void ShieldChargeAttack()
        {
            
            staminaController.RemoveStamina(75);
            moveSpeed = 10;
            anim.SetFloat("a_speed", moveSpeed * 2);
            Quaternion lookrotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, delta / rotateSpeed);
            rigid.velocity = moveDir * (moveSpeed * 2);
            weaponManager.currentWeapon.wDo.EnableShieldCollider();
            weaponManager.currentWeapon.wDo.ShieldIsTrigger();
            
        }

        //unused
        public void SlashStunAttack()
        {
            moveSpeed = 10;
            Quaternion lookrotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, delta / rotateSpeed);
            rigid.velocity = moveDir * (moveSpeed * 2);
            anim.Play("Attack1");
        }

        //checks if player is on ground and not airborne using raycasting
        public bool OnGround()
        {
            bool r = false;
            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;

            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if(Physics.Raycast(origin,dir,out hit, dis, ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;                
            }
            return r;
        }

        //unused
        public void HandleTwoHanded()
        {
            anim.SetBool("two_handed", isTwoHanded);

            if (isTwoHanded)
            {
                a_man.UpdateActionsTwoHanded();
            }
            else
            {
                a_man.UpdateActionsOneHanded();
            }
        }

        //unused
        public void LookTowardsTarget()
        {
            
            lockOnTarget = NearestEnemy();
            //Debug.Log(lockOnTarget);
            
            Vector3 dir = lockOnTarget.position - transform.position;
            //dir.Normalize();
            dir.y = 0;
            if (dir == Vector3.zero)
            {
                dir = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, delta / rotateSpeed);
        }

        //Handles damage done to the player
        public void DoDamage(float v)
        {
            a_move.DisableParryCollider();
            audioController.PlayerDamagedSound();
            health -= v;           
            
            if(health <= -100)
            {
                Debug.Log("Stoooop!!! He is already deeaaaaad!");
            }                        
            anim.applyRootMotion = true;
        }
    }


    

}
