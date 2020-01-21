using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        public GameObject activeModel;

        public float vertical;
        public float horizontal;
        public Vector3 moveDir;
        public float moveSpeed = .4f;
        public float rotateSpeed = .2f;
        public float moveAmount;

        public Animator anim;
        public Rigidbody rigid;
        Transform mTransform;

        public float delta;

        public void Init()
        {            
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            mTransform = this.transform;
            rigid.angularDrag = 999;
            rigid.drag = 4;
        }


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

        public void FixedTick(float d)
        {
            delta = d;

            rigid.drag = (moveAmount > 0) ? 0 : 4;

            rigid.velocity = moveDir * moveSpeed; 
            Vector3 lookDir = moveDir;
            if (lookDir == Vector3.zero)
            {
                lookDir = mTransform.forward;
            }
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            mTransform.rotation = Quaternion.Slerp(mTransform.rotation, lookRotation, delta / rotateSpeed);
        }
    }
}
