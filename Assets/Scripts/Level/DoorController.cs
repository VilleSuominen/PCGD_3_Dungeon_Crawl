using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject doorExit;
        int sceneIndex;

        // Start is called before the first frame update
        void Start()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            switch (sceneIndex)
            {
                case 0:
                    doorExit = GameObject.Find("Level1/LevelBorders/RoomExit");
                    break;
                case 1:
                    doorExit = GameObject.Find("Level2/RoomExit");
                    break;
                case 2:
                    doorExit = GameObject.Find("Level3/RoomBorders/RoomExit");
                    break;
                case 3:
                    doorExit = GameObject.Find("Level4/RoomBorders/RoomExit");
                    break;
                case 4:
                    doorExit = GameObject.Find("Level5/RoomBorders/RoomExit");
                    break;
            }

            doorExit.SetActive(false);
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
                    doorExit.SetActive(true);
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