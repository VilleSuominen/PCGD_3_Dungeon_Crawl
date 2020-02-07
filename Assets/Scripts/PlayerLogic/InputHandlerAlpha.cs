﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        
        float vertical; //vertical movement axis from input device
        float horizontal; //horizontal movement axis from input device
        float v_rotation;
        float h_rotation;
        bool x_input; //attack input x-button on 360 controller
        bool rb_input; //block input right bumper on 360 controller
        bool lb_input; //dodge input lef bumper on 360 controller

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
            x_input = Input.GetButtonDown("x_input");
            rb_input = Input.GetButton("RB");            
            lb_input = Input.GetButtonDown("LB");                  
            
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
            states.x = x_input; 
            states.rb = rb_input;
            states.lb = lb_input;
        }
    }
}