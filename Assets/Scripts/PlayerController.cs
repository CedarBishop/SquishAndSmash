using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;

    CharacterController controller;
    Vector2 inputDirection;
    float yVelocity;
    [HideInInspector] public Vector3 direction;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
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

}
