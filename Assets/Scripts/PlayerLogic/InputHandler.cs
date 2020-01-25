using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        float vertical;
        float horizontal;
        
        public Transform camHolder;
        public ExecutionOrder movementOrder;
        public Controller controller;
        Vector3 moveDirection;


        public enum ExecutionOrder
        {
            fixedUpdate, update, lateUpdate
        }

        
        void Start()
        {
           
        }

        //private void FixedUpdate()
        //{
        //    if(movementOrder == ExecutionOrder.fixedUpdate)
        //    {
        //        controller.Move(moveDirection, Time.fixedDeltaTime);
        //    }
        //}

        
        void Update()
        {
            GetInput();            
        }

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            moveDirection = camHolder.forward * vertical;
            moveDirection += camHolder.right * horizontal;
            moveDirection.Normalize();
            float delta = Time.deltaTime;

            if(movementOrder == ExecutionOrder.update)
            {
                controller.Move(moveDirection, Time.deltaTime);
            }
            controller.HandleMovementAnimations(moveAmount, delta);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouseclick");
                controller.Attacks();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("shield");
                controller.Shield();
            }

            

        } 
        
    }
}
