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

    public Text highscoreText;
    string fastestTimeString = "N/A";

    private void Start()
    {
        mainMenuParent.SetActive(true);
        highscoreParent.SetActive(false);
        if (PlayerPrefs.HasKey("FastestTime"))
        {
            float time = PlayerPrefs.GetFloat("FastestTime");
            fastestTimeString = time.ToString("F2");
        }
        highscoreText.text = "Fastest Time = " + fastestTimeString;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void TransistionToHighscore ()
    {
        mainMenuParent.SetActive(false);
        highscoreParent.SetActive(true);
    }

    public void TransistionToMainMenu ()
    {
        mainMenuParent.SetActive(true);
        highscoreParent.SetActive(false);
    }
}
