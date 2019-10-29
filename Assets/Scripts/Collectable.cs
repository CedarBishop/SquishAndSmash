using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Collectable : MonoBehaviour
{
    public static event System.Action<float> LowerTime;
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
            if (LowerTime != null)
            {
                LowerTime(timeToReduce);
                Destroy(gameObject);
            }
        }
    }
}
