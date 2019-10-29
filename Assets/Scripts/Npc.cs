
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string[] sentences;
    public bool isEndOfLevel;
    public bool playOnStart;

    public delegate void SendSentences(string[] Sentences, bool answer);
    public static event SendSentences sendSentences;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        if (playOnStart)
        {
            InteractNpc();
        }
    }

    public void InteractNpc()
    {
        if (sendSentences != null)
        {
            print("Invoked");
            sendSentences(sentences, isEndOfLevel);
        }
    }
}