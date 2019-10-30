using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            AudioManager.instance.Play("Sheep");
        }
    }
}
