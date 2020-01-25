using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf == false && Input.GetKeyDown("escape"))
        {
            if (pauseMenu.activeSelf == true)   // Closes the pause menu when pressing esc
            {
                pauseMenu.SetActive(false);
                Resume();
            }

            else     // Opens the pause menu if esc is pressed
            {
                pauseMenu.SetActive(true);
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }
}
