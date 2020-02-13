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

    bool allEnemiesKilled;
    bool counterOn;

    // Start is called before the first frame update
    void Start()
    {
        allEnemiesKilled = true;
        counterOn = false;
        startPosition = transform.position;
        finalPosition = new Vector3(transform.position.x, -2, transform.position.z);
        distance = Vector3.Distance(startPosition, finalPosition);
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (allEnemiesKilled)
        {
            if (counterOn == false)
            {
                startTime = Time.time;
                counterOn = true;
            }
            float distCovered = (Time.time - startTime * speed);
            float fractionOfJourney = distCovered / distance;
            transform.position = Vector3.Lerp(startPosition, finalPosition, fractionOfJourney);
        }
    }

}
}

