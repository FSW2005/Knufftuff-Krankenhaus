using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FelixMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float crouchSpeed = 1.0f;
    public float rotationSpeed = 180.0f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2.0f;
    public float sprintSpeedMultiplier = 2.0f;
    public float maxStamina = 100.0f;
    public float staminaRegenerationRate = 10.0f;

    private CharacterController characterController;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private float currentStamina;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleMovement();

        // Toggle sprint on/off with Left Shift key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSprint();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprint();
        }

        // Toggle crouch on/off with "C" key
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }

        // Regenerate stamina passively when not running
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenerationRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void HandleMovement()
    {
        // Input handling for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Determine the movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Rotate towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        // Determine the speed based on the current state (walk, sprint, crouch)
        float speed = walkSpeed;

        if (isCrouching)
        {
            speed = crouchSpeed;
        }
        else if (isSprinting && currentStamina > 0)
        {
            speed = sprintSpeed * sprintSpeedMultiplier;
            currentStamina -= Time.deltaTime;
        }

        // Move the player
        Vector3 movement = transform.forward * speed * Time.deltaTime * verticalInput;

        // Apply crouch height adjustment
        if (isCrouching)
        {
            characterController.height = crouchHeight;
        }
        else
        {
            characterController.height = standingHeight;
        }

        // Move the character controller
        characterController.Move(movement);
    }

    void StartSprint()
    {
        if (!isCrouching && currentStamina > 0)
        {
            isSprinting = true;
        }
    }

    void StopSprint()
    {
        isSprinting = false;
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;
    }
}

