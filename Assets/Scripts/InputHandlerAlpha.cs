using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        
        float vertical;
        float horizontal;
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

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            x_input = Input.GetButtonDown("x_input");
            rb_input = Input.GetButtonDown("RB");
            lb_input = Input.GetButtonDown("LB");
        }

        void UpdateStates()
        {
            states.horizontal = horizontal;
            states.vertical = vertical;            

            Vector3 v = moveAnchor.forward * vertical;
            Vector3 h = moveAnchor.right * horizontal;
            states.moveDir = (v +h).normalized;
            states.moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            states.x = x_input;
            states.rb = rb_input;
            states.lb = lb_input;
        }
    }
}