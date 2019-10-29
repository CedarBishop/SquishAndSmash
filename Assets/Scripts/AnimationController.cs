﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    PlayerController playerController;
    Detector detector;
    Vector3 lookDirection;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        detector = GetComponentInChildren<Detector>();
    }
    
    void Update()
    {
        if (playerController.direction != Vector3.zero)
        {
            lookDirection = playerController.direction;
        }
        transform.rotation = Quaternion.LookRotation(lookDirection);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    void Attack ()
    {
        if (detector.isTriggeringEnemy)
        {
            detector.triggeredEnemy.OnSquished();
        }
    }
}