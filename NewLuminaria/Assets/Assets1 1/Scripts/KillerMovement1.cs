using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillerMovement1 : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed;
    public float sprintSpeed;
    public float gravity;
    public float jumpHeight;

    bool isSprinting;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            isSprinting = false;
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);


        if (isSprinting)
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * walkSpeed * Time.deltaTime);
        }
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        } 

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
