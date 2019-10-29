using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Detector : MonoBehaviour
{
    SphereCollider sphereCollider;
    public Enemy triggeredEnemy;
    public bool isTriggeringEnemy;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            isTriggeringEnemy = true;
            triggeredEnemy = collision.GetComponent<Enemy>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            isTriggeringEnemy = false;
            triggeredEnemy = null;
        }
    }
}
