using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text timerText;
    float timer;
    void Start()
    {
        Collectable.LowerTime += (float time) => timer -= time; 
    }

    private void OnDestroy()
    {
        Collectable.LowerTime -= (float time) => timer -= time;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F1");
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            OnWin();
        }
    }

    void OnWin ()
    {
        float tempFloat;
        if (PlayerPrefs.HasKey("FastestTime"))
        {
            tempFloat = PlayerPrefs.GetFloat("FastestTime");
            PlayerPrefs.SetFloat("FastestTime", (timer < tempFloat) ? timer : tempFloat);
        }
        else
        {
            PlayerPrefs.SetFloat("FastestTime", timer);
        }
    }
}
