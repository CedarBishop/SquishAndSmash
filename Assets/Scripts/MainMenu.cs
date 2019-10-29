using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuParent;
    public GameObject highscoreParent;

    public string GameSceneName = "cedar";

    private void Start()
    {
        mainMenuParent.SetActive(true);
        highscoreParent.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void Quit ()
    {
        Application.Quit();
    }
}
