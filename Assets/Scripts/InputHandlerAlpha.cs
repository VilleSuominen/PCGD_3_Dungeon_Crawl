using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandlerAlpha : MonoBehaviour
    {
        
        float vertical;
        float horizontal;

        public Transform camHolder;
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
        }

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        void UpdateStates()
        {
            states.horizontal = horizontal;
            states.vertical = vertical;            

            Vector3 v = camHolder.forward * vertical;
            Vector3 h = camHolder.right * horizontal;
            states.moveDir = (v +h).normalized;
            states.moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        }
    }
}