using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    PlayerController playerController;
    Detector detector;
    Vector3 lookDirection = new Vector3(0,0,1);

    bool canAnimate;
    bool isPaused;

    void Start()
    {
        Dialogue.DialogueEvent += (bool answer) => canAnimate = !answer;
        PauseMenu.pauseEvent += (bool answer) => isPaused = answer;
        canAnimate = true;
        playerController = GetComponentInParent<PlayerController>();
        detector = GetComponentInChildren<Detector>();
    }

    private void OnDestroy()
    {
        Dialogue.DialogueEvent -= (bool answer) => canAnimate = !answer;
        PauseMenu.pauseEvent -= (bool answer) => isPaused = answer;
    }

    void Update()
    {
        if (isPaused == false)
        {
            if (canAnimate)
            {
                if (playerController.direction != Vector3.zero)
                {
                    lookDirection = playerController.direction;
                }
                transform.rotation = Quaternion.LookRotation(lookDirection);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Attack();
                    AudioManager.instance.Play("Swing");
                }
            }
        }       
    }

    void Attack ()
    {
        if (detector.isTriggeringEnemy)
        {
            if (detector.triggeredEnemy.enemyState != EnemyStates.Squished)
            {
                detector.triggeredEnemy.OnSquished();                
            }
        }
    }
}
