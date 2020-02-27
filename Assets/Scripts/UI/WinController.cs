using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SA
{
    public class WinController : MonoBehaviour
    {

        int enemiesAlive;
        bool allEnemiesKilled;
        GameObject[] enemies;
        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Controller");
        }

        // Update is called once per frame
        void Update()
        {
            if (allEnemiesKilled == false)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies == null)
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
                GetComponent<Text>().text = "YOU WIN!";
                StartCoroutine("Reset");
            }
        }

        IEnumerator Reset()
        {
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(0);
        }
    }
}