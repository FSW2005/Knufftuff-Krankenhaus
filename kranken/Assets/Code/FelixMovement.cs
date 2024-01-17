using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelixMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
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

    private Camera playerCamera;
    private bool isRotatingCamera = false;
    private Vector2 lastMousePosition;
    // Bas rörelse för spelarkontrolen
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        currentStamina = maxStamina;
        // Addar "stamina" komponenten
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleActions();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (verticalInput * cameraForward + horizontalInput * playerCamera.transform.right).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        float speed = moveSpeed;

        if (isCrouching)
        {
            speed /= 2.0f;
        }
        else if (isSprinting && currentStamina > 0)
        {
            speed *= sprintSpeedMultiplier;
            currentStamina -= Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            // Återhämta och få tillbaka "Stamina" då en inte springer
            currentStamina += staminaRegenerationRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        Vector3 movement = moveDirection * speed * Time.deltaTime;
        characterController.Move(movement);
    }
    // Mer detaljerad rörelse för spelarkontrolen
    void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotatingCamera = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotatingCamera = false;
        }

        if (isRotatingCamera)
        {
            Vector2 deltaMousePosition = (Vector2)Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            float rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            playerCamera.transform.Rotate(Vector3.up * rotationY);
            playerCamera.transform.Rotate(Vector3.right * rotationX);

            // Kan begränsa kamerans rotation på X-axeln som bör stoppa den från att göra vålt
            Vector3 currentRotation = playerCamera.transform.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, -90.0f, 90.0f);
            playerCamera.transform.eulerAngles = currentRotation;
        }
    }
    // Kamerans rotations funktioner, kameran ska följa musen
    void HandleActions()
    {
        // Sprint ("Left Shift")
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartSprint();
        }
        else
        {
            StopSprint();
        }

        // Crouch ("Left Ctrl")
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            characterController.height = crouchHeight;
        }
        else
        {
            characterController.height = standingHeight;
        }
    }
    // Ändrar höjden på spelaren
    void StartSprint()
    {
        if (!isCrouching && currentStamina > 0)
        {
            isSprinting = true;
        }
    }
    // kan inte göra "Sprint" då en är i "crouch"
    void StopSprint()
    {
        isSprinting = false;
    }
    // Om en inte håller "Left Shift" springer man inte
}
