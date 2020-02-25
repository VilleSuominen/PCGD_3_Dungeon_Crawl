using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SA
{
    public class MenuController : MonoBehaviour
    {
        EventSystem eventSystem;

        GameObject mainMenu;
        GameObject pauseMenu;
        GameObject helpMenu;
        GameObject dialogPanel;
        GameObject statusPanel;
        GameObject logo;

        public GameObject lastMenu;

        // Start is called before the first frame update
        void Start()
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            mainMenu = transform.Find("MainMenuPanel").gameObject;
            pauseMenu = transform.Find("PauseMenuPanel").gameObject;
            helpMenu = transform.Find("HelpMenuPanel").gameObject;
            dialogPanel = transform.Find("DialogPanel").gameObject;
            statusPanel = transform.Find("StatusPanel").gameObject;
            logo = transform.Find("Logo").gameObject;

            if (mainMenu.activeSelf == true)
            {
                Time.timeScale = 0.0f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mainMenu.activeSelf == false && dialogPanel.activeSelf == false && Input.GetButtonDown("Cancel"))
            {
                if (pauseMenu.activeSelf == true && helpMenu.activeSelf == false)   // Closes the pause menu when pressing esc
                {
                    pauseMenu.SetActive(false);
                    statusPanel.SetActive(true);
                    Resume();
                }

                else     // Opens the pause menu if esc is pressed
                {
                    pauseMenu.SetActive(true);
                    statusPanel.SetActive(false);
                    Pause();
                }
            }

            else if (mainMenu.activeSelf == true && logo.activeSelf == false && helpMenu.activeSelf == false)
            {
                logo.SetActive(true);
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

        public void SetLastMenu(GameObject g)
        {
            lastMenu = g;
        }

        public void OpenLastMenu()
        {
            lastMenu.SetActive(true);
        }
    }
}