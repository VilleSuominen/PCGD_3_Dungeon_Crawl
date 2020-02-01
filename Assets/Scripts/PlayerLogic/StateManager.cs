﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    //players movement/animation states happen in this class
    public class StateManager : MonoBehaviour
    {
        public GameObject activeModel;

        [Header("inputs")]
        public float vertical;
        public float horizontal;
        public float v_rot;
        public float h_rot;
        public Vector3 lookDir;
        public bool rb, lb, x;
        public Vector3 moveDir;
        public float moveSpeed = 5f;
        public float rotateSpeed = .02f;
        public float moveAmount;
        public float toGround = .5f;

        int floorMask;
        float camRayLength = 100f;

        [Header("States")]
        public bool onGround;
        public bool inAction;
        public bool canMove;
        public bool isTwoHanded;
        public bool isBlocking;

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
        Transform mTransform;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        float _delay;

        public void Init()
        {            
            SetupAnimator();
            floorMask = LayerMask.GetMask("Floor");
            rigid = GetComponent<Rigidbody>();
            mTransform = this.transform;
            rigid.angularDrag = 999;
            rigid.drag = 4;

            weaponManager = GetComponent<WeaponManager>();
            weaponManager.Init();

            a_man = GetComponent<ActionManager>();
            a_man.Init(this);

            a_move = activeModel.GetComponent<AnimationMove>();
            if(a_move == null)
            {
                a_move = activeModel.AddComponent<AnimationMove>();
            }           
            a_move.Init(this, null);

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
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

            if (onGround)
            {                
                if(isBlocking == true)
                {
                    moveSpeed = 1f;
                }
                else
                {
                    moveSpeed = 5f;
                }
                rigid.velocity = moveDir * (moveSpeed * moveAmount);
            }
            
            Vector3 lookDirection = lookDir;
            if (lookDir == Vector3.zero)
            {
                //lookDir = mTransform.forward;
                lookDir = transform.forward;
            }
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, delta / rotateSpeed);
            //Turning();
            MovementAnimationHandler();
        }

        //checks what action/animation can be done according to given conditions
        public void CheckAction()
        {
            if(canMove == false)
            {
                return;
            }

            if(rb == false && rb == false && x == false)
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

        void AttackAction(Action slot)
        {
            string targetAnim = null;
            targetAnim = slot.targetAnim;

            if (string.IsNullOrEmpty(targetAnim))
            {
                return;
            }

            canMove = false;
            inAction = true;            

            anim.CrossFade(targetAnim, 0.2f);
            //rigid.velocity = Vector3.zero;
        }

        void BlockAction(Action slot)
        {            
            isBlocking = true;
        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
        }

        //plays movement animations
        void MovementAnimationHandler()
        {
            anim.SetFloat("vertical", moveAmount, 0.2f, delta);

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
                //targetPosition.y += toGround;
            }
            return r;
        }

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

        void Turning()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                rigid.MoveRotation(newRotation);


            }
        }
    }


}
