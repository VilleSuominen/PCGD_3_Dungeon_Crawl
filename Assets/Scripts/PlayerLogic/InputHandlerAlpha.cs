using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        NewControls controls;
        Vector2 movement; //vertical movement axis from input device
        Vector2 rotation; //horizontal movement axis from input device       
        bool attack; //attack input button 5        
        bool block; //block input right bumper on 360 controller
        bool backstep; //backstep input lef bumper on 360 controller
        bool charge;
        bool lockOn;

        public Transform moveAnchor;
        float delta;
        StateManager states;

        private void Awake()
        {
            controls = new NewControls();
        }

        private void OnEnable()
        {
            controls.PlayerInputPad.Block.Enable();
            controls.PlayerInputPad.Attack.Enable();
            controls.PlayerInputPad.Movement.Enable();
            controls.PlayerInputPad.Rotation.Enable();
            controls.PlayerInputPad.BackDash.Enable();
            controls.PlayerInputPad.ShieldCharge.Enable();
            controls.PlayerInputPad.LockOn.Enable();
            controls.PlayerInputPad.LockOn.performed += cont => lockOn = true;
            controls.PlayerInputPad.LockOn.canceled += cont => lockOn = false;
            controls.PlayerInputPad.BackDash.performed += cont => backstep = true;
            controls.PlayerInputPad.BackDash.canceled += cont => backstep = false;
            controls.PlayerInputPad.ShieldCharge.performed += cont => charge = true;
            controls.PlayerInputPad.ShieldCharge.canceled += cont => charge = false;
            controls.PlayerInputPad.Block.performed += cont => block = true;
            controls.PlayerInputPad.Block.canceled += cont => block = false;
            controls.PlayerInputPad.Attack.performed += cont => attack = true;
            controls.PlayerInputPad.Attack.canceled += cont => attack = false;
            controls.PlayerInputPad.Movement.performed += cont => movement = cont.ReadValue<Vector2>();
            controls.PlayerInputPad.Movement.canceled += cont => movement = Vector2.zero;
            controls.PlayerInputPad.Rotation.performed += cont => rotation = cont.ReadValue<Vector2>();
            controls.PlayerInputPad.Rotation.canceled += cont => rotation = Vector2.zero;
        }

        private void OnDisable()
        {

            controls.PlayerInputPad.Block.Disable();
            controls.PlayerInputPad.Attack.Disable();
            controls.PlayerInputPad.Movement.Disable();
            controls.PlayerInputPad.Rotation.Disable();
            controls.PlayerInputPad.BackDash.Disable();
            controls.PlayerInputPad.ShieldCharge.Disable();
            controls.PlayerInputPad.LockOn.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            GameObject anchor = GameObject.FindGameObjectWithTag("Anchor");
            moveAnchor = anchor.transform;
            states = GetComponent<StateManager>();
            states.Init();
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
            states.backstep = backstep;
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