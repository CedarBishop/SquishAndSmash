using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialoige : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GetComponent<Npc>().InteractNpc();
        }
    }
}
