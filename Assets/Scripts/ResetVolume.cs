using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVolume : MonoBehaviour
{
    public GameObject reset;
    public GameObject player;
    void Update()
    {
        if (player.transform.position.y <= 1)
        {
            player.transform.position = reset.transform.position;
            player.transform.rotation = reset.transform.rotation;
        }
    }
}
