using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AnimDebugger : MonoBehaviour
    {
        [Range(0,1)] 
        public float vertical;

        //public string [] oh_attacks; //one handed attacks
        //public string [] th_attacks; //two handed attacks
        public string animName;
        public bool playAnim;
        //public bool twoHanded;

        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (playAnim)
            {                              
                vertical = 0;
                anim.CrossFade(animName, 0.2f);
                playAnim = false;
            }
            anim.SetFloat("vertical", vertical);
        }
    }
}
