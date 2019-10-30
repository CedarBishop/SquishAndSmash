using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    PlayerController playerController;
    Detector detector;
    Animator animator;
    
    public Vector3 lookDirection = new Vector3(0,0,1);

    bool canAnimate;
    bool isPaused;

    void Start()
    {
        animator = GetComponent<Animator>();
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
                animator.SetFloat("Speed", Mathf.Abs(playerController.direction.magnitude));
                if (playerController.direction != Vector3.zero)
                {
                    lookDirection = playerController.direction;
                }
                transform.rotation = Quaternion.LookRotation(lookDirection);

                if (Input.GetKeyDown(KeyCode.O))
                {
                    animator.SetTrigger("Swing");
                    AudioManager.instance.Play("Swing");
                }
            }
        }       
    }

    public void Impact ()
    {
        if (detector.isTriggeringEnemy)
        {
            if (detector.triggeredEnemy.enemyState != EnemyStates.Squished)
            {
                detector.triggeredEnemy.OnSquished();                
            }
        }
        if (detector.isTriggeringHammer)
        {
            detector.triggeredHammer.PlaySmashAnim();
        }
    }

    public void StartOnHitAnim ()
    {
        animator.SetTrigger("OnHit");
    }

    public void EndOnHitAnim()
    {
        playerController.CanMoveAgain();
    }
}
