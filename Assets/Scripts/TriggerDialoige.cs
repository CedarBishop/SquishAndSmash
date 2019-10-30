using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialoige : MonoBehaviour
{
    private bool played = false;
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.GetComponent<PlayerController>())
        {
            if (!played)
            {
                GetComponent<Npc>().InteractNpc();
                played = true;
            }
        }
    }
}
