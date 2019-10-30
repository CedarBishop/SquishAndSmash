using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Detector : MonoBehaviour
{
    SphereCollider sphereCollider;
    public Enemy triggeredEnemy;
    public bool isTriggeringEnemy;
    public bool isTriggeringHammer;
    public Hammer triggeredHammer;
    bool isGrabbingEnemy;
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
        if (collision.gameObject.GetComponent<Hammer>())
        {
            isTriggeringHammer = true;
            triggeredHammer = GetComponent<Hammer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            isTriggeringEnemy = false;
            triggeredEnemy = null;
        }
        if (other.gameObject.GetComponent<Hammer>())
        {
            isTriggeringHammer = true;
            triggeredHammer = null;
        }
    }

    private void Update()
    {
        if (isGrabbingEnemy)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                isGrabbingEnemy = false;
                if (triggeredEnemy != null)
                {
                    triggeredEnemy.transform.parent = null;
                }
            }
        }
        else if (isTriggeringEnemy)
        {
            if (triggeredEnemy != null)
            {
                if (triggeredEnemy.enemyState == EnemyStates.Squished)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        if (isGrabbingEnemy == false)
                        {
                            isGrabbingEnemy = true;
                            triggeredEnemy.transform.parent = transform;
                        }
                    }
                }
                else
                {
                    isGrabbingEnemy = false;
                    triggeredEnemy.transform.parent = null;
                }
            }
        }

        
        
    }
}
