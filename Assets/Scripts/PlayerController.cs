using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;

    CharacterController controller;
    Vector3 inputDirection;
    Vector3 direction;

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
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"),controller.velocity.y, Input.GetAxisRaw("Vertical"));
        direction = inputDirection.normalized;
    }

    void Move()
    {
        controller.Move(direction * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            direction.y = jumpSpeed;
        }
        direction.y -= gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
