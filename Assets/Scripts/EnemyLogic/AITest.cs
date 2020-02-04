using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SA {

    public class AITest : MonoBehaviour
    {

        public GameObject player;
        public NavMeshAgent agent;
        EnemyStates eStates;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Controller");
            agent = GetComponent<NavMeshAgent>();
            eStates = GetComponent<EnemyStates>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 playerPos = (player.transform.position);
            agent.SetDestination(playerPos);
            if(0<agent.remainingDistance && agent.remainingDistance <= agent.stoppingDistance + 2)
            {
                eStates.anim.Play("Attack");
            }
        }
    }
}