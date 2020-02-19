using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {

    public class DoorController : MonoBehaviour
    {

        Vector3 finalPosition;
        Vector3 startPosition;
        Vector3 direction;
        float distance;
        float startTime;
        float speed;

        int enemiesAlive;
        bool allEnemiesKilled;
        bool counterOn;
        GameObject[] enemies;

        // Start is called before the first frame update
        void Start()
        {            
            allEnemiesKilled = false;
            counterOn = false;
            startPosition = transform.position;
            finalPosition = new Vector3(transform.position.x, -2, transform.position.z);
            distance = Vector3.Distance(startPosition, finalPosition);
            speed = 1f;
        }

        // Update is called once per frame
        void Update()
        {  
            if (allEnemiesKilled == false)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if(enemies == null)
                {
                    allEnemiesKilled = true;
                    return;
                }
                enemiesAlive = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<EnemyStates>().isDead == false)
                    {
                        enemiesAlive++;
                    }
                }
                if (enemiesAlive == 0)
                {
                    allEnemiesKilled = true;
                }
            }

            else
            {
                DoorOpening();
            }
        }

        void DoorOpening()
        {
            if (counterOn == false)
            {
                startTime = Time.time;
                counterOn = true;
            }
            float distCovered = Time.time - startTime * speed;
            float fractionOfJourney = distCovered / distance;
            transform.position = Vector3.Lerp(startPosition, finalPosition, fractionOfJourney);
        }
    }

}