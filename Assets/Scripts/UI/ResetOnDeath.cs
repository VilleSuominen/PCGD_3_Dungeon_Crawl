using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetOnDeath : MonoBehaviour
{
    string text;
        
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        if(text == "YOU DIED")
        {
            StartCoroutine("Reset");
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(0);
    }
}
