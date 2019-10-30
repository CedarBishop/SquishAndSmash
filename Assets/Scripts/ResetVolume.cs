using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVolume : MonoBehaviour
{
    public GameObject reset;
    private void OnTriggerExit(Collider other)
    {
        other.transform.position = reset.transform.position;
        other.transform.rotation = reset.transform.rotation;
    }
}
