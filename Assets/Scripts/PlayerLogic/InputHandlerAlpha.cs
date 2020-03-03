using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        Vector2 movement; //vertical movement axis from input device
        Vector2 rotation; //horizontal movement axis from input device        
        bool attack; //attack input button 5        
        bool block; //block input right bumper on 360 controller
        //bool backstep; //backstep input lef bumper on 360 controller
        bool charge;
        bool lockOn;
        float attackv;
        float blockV;
        float backstepV;
        float chargeV;
        float lockOnV;

        public Transform moveAnchor;
        float delta;
        StateManager states;

        private void OnShieldCharge(InputValue value)
        {
            chargeV = value.Get<float>();
        }

        void OnBackDash(InputValue value)
        {
            backstepV = value.Get<float>();
        }

        void OnLockOn(InputValue value)
        {
            lockOnV = value.Get<float>();
        }

        private void OnMovement(InputValue value)
        {
            movement = value.Get<Vector2>();
        }

        private void OnRotation(InputValue value)
        {
            rotation = value.Get<Vector2>();
        }

        private void OnAttack(InputValue value)
        {
            attackv = value.Get<float>();
        }

        private void OnBlock(InputValue value)
        {
            blockV = value.Get<float>();
        }

        // Start is called before the first frame update
        void Start()
        {
            GameObject anchor = GameObject.FindGameObjectWithTag("Anchor");
            moveAnchor = anchor.transform;
            states = GetComponent<StateManager>();
            states.Init();
            
            if (this.CompareTag("DeadPlayer"))
            {
                Destroy(gameObject);
            }
            
        }
        void CheckActions()
        {
            attack = (attackv >= 0.9) ? true : false;
            block = (blockV >= 0.9) ? true : false;
            charge = (chargeV >= 0.9) ? true : false;
            //backstep = (backstepV >= 0.9) ? true : false;
            lockOn = (lockOnV >= 0.9) ? true : false;

        }
        // Update is called once per frame
        void FixedUpdate()
        {

            if (moveAnchor == null)
            {
                GameObject anchor = GameObject.FindGameObjectWithTag("Anchor");
                moveAnchor = anchor.transform;
            }
            delta = Time.fixedDeltaTime;
            UpdateStates();
            states.FixedTick(delta);
        }

        private void Update()
        {
            CheckActions();
            delta = Time.deltaTime;
            states.Tick(delta);
        }

        //updates player action states from the statemanager
        void UpdateStates()
        {
            states.rotation = rotation;
            states.movement = movement;

            Vector3 v = moveAnchor.forward * movement.y;
            Vector3 h = moveAnchor.right * movement.x;
            Vector3 vr = moveAnchor.forward * rotation.y;
            Vector3 hr = moveAnchor.right * rotation.x;
            states.lookDir = (vr + hr);
            states.moveDir = (v + h).normalized; //passes the normalized movement to the statemanager class moveDir variable
            states.moveAmount = Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.y)); //Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range

            //passes the input button variables to the statemanager class

            states.attack = attack;
            states.block = block;
            //states.backstep = backstep;
            states.charge = charge;

            if (lockOn)
            {
                states.lockOn = !states.lockOn;
                if (states.lockOnTarget == null)
                {
                    states.lockOn = false;
                }
            }
        }
    }
}