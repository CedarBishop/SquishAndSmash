using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 7;

    AnimationController animationController;
    CharacterController controller;
    Vector2 inputDirection;
    float yVelocity;
    [HideInInspector] public Vector3 direction;

    public float jumpSpeed = 12;
    public float gravity = 1.5f;

    bool canMove;
    bool isTalking;

    void Start()
    {
        Dialogue.DialogueEvent += (bool answer) => isTalking = answer; 
        controller = GetComponent<CharacterController>();
        animationController = GetComponentInChildren<AnimationController>();
        canMove = true;
    }

    private void OnDestroy()
    {
        Dialogue.DialogueEvent -= (bool answer) => isTalking = answer;
    }

    void Update()
    {
        if (canMove)
        {
            GetInput();
        }
    }

    private void FixedUpdate()
    {
        if (isTalking == false)
        {
            Move();
            Jump();
        }
    }

    void GetInput()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDirection = inputDirection.normalized;
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                yVelocity = jumpSpeed;
                AudioManager.instance.Play("Jump");
            }

        }
        else if (controller.isGrounded == false)
        {
            yVelocity -= gravity;
        }


        direction = new Vector3(inputDirection.x, 0,inputDirection.y);
    }

    void Move()
    {
        controller.Move(direction * speed * Time.fixedDeltaTime);
    }

    void Jump ()
    {
        controller.Move(new Vector3(0, yVelocity, 0) * Time.fixedDeltaTime);
    }

    public void PlayerHit ()
    {
        AudioManager.instance.Play("PlayerHit");
        canMove = false;
        controller.Move((animationController.lookDirection * -1));
        animationController.StartOnHitAnim();
    }

    public void CanMoveAgain ()
    {
        canMove = true;
    }
    

}
