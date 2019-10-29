using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuParent;
    bool isPaused;
    public string mainMenuName = "MainMenu";
    void Start()
    {
        pauseMenuParent.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

     public void Resume ()
     {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenuParent.SetActive(false);
     }

    void Pause ()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenuParent.SetActive(true);
    }

    public void TransitionToMainMenu ()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    
}
