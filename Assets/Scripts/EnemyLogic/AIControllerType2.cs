using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    public class AIControllerType2 : MonoBehaviour
    {
        public AIAttack[] ai_attack;
        public EnemyStates eStates;
        public Transform target;
        public StateManager states;
        GameObject player;
        public float sight;
        public int closeCount = 10;
        int _close;
        public float fov_angle;
        public int frameCount = 30;
        int _frame;
        public int attackCount = 30;
        int _attack;
        float dist;
        

        public float angle;
        Vector3 targetDir;
        float delta;

        float distanceFromTarget()
        {
            if (target == null)
            {
                return 100;
            }
            return Vector3.Distance(target.position, transform.position);
        }

        float angleToTarget()
        {
            float a = 180;
            if (target)
            {
                Vector3 d = targetDir;
                a = Vector3.Angle(d, transform.forward);
            }
            return a;
        }

        private void Awake()
        {
            if (eStates == null)
            {
                eStates = GetComponent<EnemyStates>();
            }
            eStates.Init();
            
        }

        private void Start()
        {

            
            player = GameObject.FindGameObjectWithTag("Player");
            if ( player == null)
            {
                return;
            }            
            states = player.GetComponent<StateManager>();
            target = player.transform;
            
        }

        public AIState aiState;

        public enum AIState
        {
            far, close, inSight, attacking
        }

        private void Update()
        {
            if(player == null||player.CompareTag("DeadPlayer"))
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player == null)
                {
                    
                    return;
                }
                states = player.GetComponent<StateManager>();
                target = player.transform;      
                
            }            
            
            if (!target)
            {
                
                return;
            }
            if (eStates.agent.isStopped)
            {
                eStates.anim.SetFloat("vertical", 0);
            }
            delta = Time.deltaTime;
            dist = distanceFromTarget();
            angle = angleToTarget();

            if (target)
            {
                targetDir = target.position - transform.position;
            }
            
            if (eStates.hitEnemy && eStates.canMove)
            {
                aiState = AIState.inSight;
            }
            //if(eStates.takesDamage && !eStates.canMove)
            //{
            //    aiState = AIState.far;
            //}
            switch (aiState)
            {
                case AIState.far:
                    HandleFarSight();
                    break;
                case AIState.close:
                    HandleCloseSight();
                    break;
                case AIState.inSight:
                    InSight();
                    break;
                case AIState.attacking:
                    if (eStates.canMove&&!states.isDead)
                    {
                        aiState = AIState.inSight;
                    }
                    break;
                default:
                    break;
            }
            eStates.Tick();
        }

        //Enemys close range state
        void HandleCloseSight()
        {
            _close++;
            if (_close > closeCount)
            {
                _close = 0;

                if (dist > sight || angle > fov_angle)
                {
                    aiState = AIState.far;
                    return;
                }

            }
            RayCastToTarget();

        }

        void GoToDestination()
        {
            eStates.hasDestination = false;
            eStates.SetDestination(target.position);

        }

        //Enemys state when player is insight
        void InSight()
        {

            LookTowardsTarget();
            HandleCoolDowns();
            float d2 = Vector3.Distance(eStates.targetDestination, target.position);
            if (d2 > 1.2 || dist > sight * 0.25)
            {

                GoToDestination();
            }
            if (dist < 1.2)
            {
                eStates.agent.isStopped = true;
            }


            if (_attack > 0)
            {
                _attack--;
                return;
            }
            _attack = attackCount;
            AIAttack attack = HandleAttacks();
            if (attack != null)
            {
                aiState = AIState.attacking;
                eStates.anim.SetFloat("speed", attack.animSpeed * eStates.agent.speed);
                eStates.anim.Play(attack.targetAnim);
                eStates.anim.SetBool("canMove", false);
                eStates.canMove = false;
                attack._cool = attack.cooldown;
                eStates.agent.isStopped = true;
                return;
            }

        }

        //Takes care of the cooldown of attacks
        void HandleCoolDowns()
        {
            for (int i = 0; i < ai_attack.Length; i++)
            {
                AIAttack a = ai_attack[i];
                if (a._cool > 0)
                {
                    a._cool -= delta;
                    if (a._cool < 0)
                    {
                        a._cool = 0;
                    }
                    continue;
                }
            }
        }

        //Turns the enemy towards the player
        void LookTowardsTarget()
        {
            Vector3 dir = targetDir;
            dir.y = 0;
            if (dir == Vector3.zero)
            {
                dir = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, delta * 2);
        }

        //chooses what attack the enemy does
        public AIAttack HandleAttacks()
        {
            int w = 0;
            List<AIAttack> l = new List<AIAttack>();
            for (int i = 0; i < ai_attack.Length; i++)
            {
                AIAttack a = ai_attack[i];
                if (a._cool > 0)
                {

                    continue;
                }

                if (a.minDistance < dist)
                {
                    continue;
                }
                if (angle < a.minAngle)
                {
                    continue;
                }
                if (angle > a.maxAngle)
                {
                    continue;
                }
                if (a.weight == 0)
                {
                    continue;
                }
                w += a.weight;
                l.Add(a);
            }
            if (l.Count == 0)
            {
                return null;
            }

            int ran = Random.Range(0, w + 1);
            int cw = 0;
            for (int i = 0; i < l.Count; i++)
            {
                cw += l[i].weight;
                if (cw > ran)
                {
                    return l[i];
                }
            }
            return null;
        }

        //raycasts player when in range
        void RayCastToTarget()
        {
            RaycastHit hit;
            Vector3 origin = transform.position;
            origin.y += 2.5f;
            Vector3 dir = targetDir;

            dir.y -= 1.0f;

            if (Physics.Raycast(origin, dir, out hit, sight, eStates.ignoreLayers))
            {

                StateManager st = hit.transform.GetComponentInParent<StateManager>();

                if (st != null)
                {
                    aiState = AIState.inSight;
                    eStates.SetDestination(target.position);

                }
            }

        }

        //when player is faaaaaaaaaaaaaar
        void HandleFarSight()
        {
            if (target == null)
            {
                
                return;
            }
            
            _frame++;

            if (_frame > frameCount)
            {
                _frame = 0;

                if (dist < sight)
                {
                    if (angle < fov_angle)
                    {
                        aiState = AIState.close;
                    }
                }
            }
        }

    }

}