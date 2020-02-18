using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        
        float vertical; //vertical movement axis from input device
        float horizontal; //horizontal movement axis from input device
        float v_rotation; //vertical rotation axis
        float h_rotation; //horizontal rotation axis
        bool attack; //attack input button 5
        bool block; //block input right bumper on 360 controller
        bool backstep; //backstep input lef bumper on 360 controller
        bool charge;

        public Transform moveAnchor;
        float delta;

        StateManager states;

        // Start is called before the first frame update
        void Start()
        {
            states = GetComponent<StateManager>();
            states.Init();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            UpdateStates();
            states.FixedTick(delta);
            
        }

        private void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);
            
        }

        //Gets the user input
        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            v_rotation = Input.GetAxis("v_rot");
            h_rotation = Input.GetAxis("h_rot");
            attack = Input.GetButtonDown("attack");
            block = Input.GetButton("block");            
            //backstep = Input.GetButtonDown("backstep");
            //charge = Input.GetButtonDown("charge");
            
        }

        //updates player action states from the statemanager
        void UpdateStates()
        {
            states.horizontal = horizontal; //passes the horizontal axis input variable to the statemanager class
            states.vertical = vertical; //passes the vertical axis input variable to the statemanager class   
            states.v_rot = v_rotation;
            states.h_rot = h_rotation;

            Vector3 v = moveAnchor.forward * vertical; //the players movement vector vertical from the transform of the moveanchor object
            Vector3 h = moveAnchor.right * horizontal; //the players movement vector horizontal from the transform of the moveanchor object
            Vector3 vr = moveAnchor.forward * v_rotation;
            Vector3 hr = moveAnchor.right * h_rotation;
            states.lookDir = (vr + hr);
            states.moveDir = (v +h).normalized; //passes the normalized movement to the statemanager class moveDir variable
            states.moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range

            //passes the input button variables to the statemanager class
            states.attack = attack; 
            states.block = block;
            states.backstep = backstep;
            states.charge = charge;
        }
    }
}