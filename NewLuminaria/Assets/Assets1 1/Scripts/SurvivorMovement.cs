using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivorMovement : MonoBehaviour
{

    public CharacterController controller;

    public float walkSpeed;
    public float sprintSpeed;
    public float gravity;
    public float jumpHeight;
    public float jumpStaminaCost;

    public Slider staminaSlider;

    public float noStaminaJumpHeight;

    public float noStaminaDelay;
    bool noStaminaGain = false;

    float singleFrameTime = 0.06f;

    public float Maxstamina;
    public float stamina;
    public float staminaDrain;
    public float staminaRegain;
    public float regainStaminaDelay;

    public float slowWalkSpeed;
    bool isSlowWalking;

    public bool canMove;

    bool isSprinting;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded;

    Vector3 move;

    bool isCrouching;
    public float crouchSpeed;
    public float crouchYScale;

    float StandingYScale;
    
    // Start is called before the first frame update
    void Start()
    {
        stamina = Maxstamina;
        staminaSlider.maxValue = Maxstamina;
        StandingYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        stamina = Mathf.Clamp(stamina, 0, Maxstamina);

        move = Vector3.Normalize(transform.right * x + transform.forward * z);
     
        if(Maxstamina > stamina && !isSprinting && !noStaminaGain)
        {
            StartCoroutine(staminaRegen());
        }
        else if (isSprinting && !noStaminaGain)
        {
            StartCoroutine(staminaDraining());
        }

        if(stamina <= 0)
        {
            stamina = 0;
            StartCoroutine(NoStamina());
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (canMove)
        {
            if(isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, StandingYScale, transform.localScale.z);
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            controller.Move(move * CurrentState() * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded && !isSlowWalking)
            {
                velocity.y = Mathf.Sqrt(isNoStaminaJump() * -2 * gravity);
                stamina -= jumpStaminaCost;
            }
        }
        else
        {
            isSlowWalking = false;
            isSprinting = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        staminaSlider.value = stamina;
    }

    IEnumerator staminaRegen()
    {
        stamina += staminaRegain;

        yield return new WaitForSeconds(singleFrameTime);
    }
    IEnumerator staminaDraining()
    {
        stamina -= staminaDrain;

        yield return new WaitForSeconds(singleFrameTime);
    }
    IEnumerator NoStamina()
    {
        noStaminaGain = true;

        for (float i = 0; i < noStaminaDelay; i += singleFrameTime)
        {
            if (stamina <= 0)
            {
                yield return new WaitForSeconds(singleFrameTime);
            }
            else
            {
                noStaminaGain = false;
                yield break;
            }
        }
        noStaminaGain = false;
    }
    float isNoStaminaJump()
    {
        if(!noStaminaGain)
        {
            return jumpHeight;
        }
        else
        {
            return noStaminaJumpHeight;
        }
    }
    float CurrentState()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !isSlowWalking && move != Vector3.zero)
        {
            isSprinting = true;
            return sprintSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftControl) && !isSprinting)
        {
            isSlowWalking = true;
            return slowWalkSpeed;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            return crouchSpeed;
        }
        else
        {
            isSlowWalking = false;
            isSprinting = false;
            isCrouching = false;
            return walkSpeed;
        }
    }
}
