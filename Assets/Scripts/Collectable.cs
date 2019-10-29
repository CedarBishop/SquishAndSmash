using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Collectable : MonoBehaviour
{
    SphereCollider collider;
    public float timeToReduce;
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {

        }
    }
}
