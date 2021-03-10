using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 32f;
    public float gravityValue = -9.81f;
    public float jumpHeight = 12f;

    private Vector3 moveDirection;
    bool isGrounded;
    private float velocityV;

    private void Awake()
    {
        controller = GetComponent <CharacterController>();
    }

    void Move()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.horizontal), 0f, Input.GetAxis(Axis.vertical));

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed * Time.deltaTime;

        Gravity();

        controller.Move(moveDirection);


        //print("HORIZONTAL: " + Input.GetAxis("Horizontal")); debugging purposes
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Gravity()
    {
        velocityV += gravityValue * Time.deltaTime;

        Jump();

        moveDirection.y = velocityV * Time.deltaTime;
    }

    void Jump()
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityV = jumpHeight;
        }
    }

}
