using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FelixMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2.0f;
    public float sprintSpeedMultiplier = 2.0f;

    private CharacterController characterController;
    private bool isSprinting = false;
    private bool isCrouching = false;

    private Camera playerCamera;
    private bool isRotatingCamera = false;
    private Vector2 lastMousePosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (verticalInput * cameraForward + horizontalInput * playerCamera.transform.right).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        float speed = moveSpeed;

        if (isCrouching)
        {
            speed /= 2.0f;
        }
        else if (isSprinting && Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintSpeedMultiplier;
        }

        Vector3 movement = moveDirection * speed * Time.deltaTime;
        characterController.Move(movement);
    }

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

            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            playerCamera.transform.Rotate(Vector3.up * rotationY);
            playerCamera.transform.Rotate(Vector3.right * rotationX);

            // Optionally, you can limit the camera rotation in the X-axis to prevent flipping
            Vector3 currentRotation = playerCamera.transform.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, 0.0f, 90.0f);
            playerCamera.transform.eulerAngles = currentRotation;
        }
    }
}
