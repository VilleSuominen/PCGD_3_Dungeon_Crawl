using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{    
    public class Controller : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public float moveSpeed = .4f;
        public float rotateSpeed = .2f;
        public float vertical;

        public GameObject activeModel;
        Transform mTransform;
        public Animator anim;
        
        

        public void Start()
        {        
            
            anim = GetComponentInChildren<Animator>();    
            
            
            mTransform = this.transform;
            rigidbody = GetComponent<Rigidbody>();
            
        }

        
        void Update()
        {

        }


        public void Move(Vector3 moveDirection, float delta)
        {            
            rigidbody.velocity = moveDirection * moveSpeed;
            Vector3 lookDir = moveDirection;
            if(lookDir == Vector3.zero)
            {
                lookDir = mTransform.forward;
            }
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            mTransform.rotation = Quaternion.Slerp(mTransform.rotation, lookRotation, delta / rotateSpeed);
        }

        public void HandleMovementAnimations(float moveAmount, float delta)
        {
            float m = moveAmount;
            
            anim.SetFloat("vertical", m, 0.2f, delta);
        }

        public void Shield()
        {
            anim.CrossFade("shield", 0.4f);
        }

        public void Attacks()
        {
            
            anim.CrossFade("oh_attack_1", 0.4f);                    
             
            
        }
    }
}
