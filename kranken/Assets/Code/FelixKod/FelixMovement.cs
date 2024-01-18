using System.Collections;
using UnityEngine;

public class FelixMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;
    public float rotationSpeed = 2f;
    public float yRotationLimit = 88f;
    public float staminaMax = 100f;
    public float staminaRegenRate = 10f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 1.7f;
    public float crouchTransitionDelay = 0.5f;

    private float currentSpeed;
    private float currentStamina;
    private bool isSprinting;
    private bool isCrouching;
    private Coroutine crouchCoroutine;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = walkSpeed;
        currentStamina = staminaMax;
        isSprinting = false;
        isCrouching = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleActions();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (isSprinting && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentStamina -= Time.deltaTime;
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentStamina += Time.deltaTime * staminaRegenRate;
            currentSpeed = isCrouching ? crouchSpeed : walkSpeed;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, staminaMax);

        // Get the y-rotation of the camera
        float cameraYRotation = Camera.main.transform.eulerAngles.y;

        // Calculate the move direction based on camera's y-rotation
        Vector3 moveDirection = Quaternion.Euler(0f, cameraYRotation, 0f) * direction;

        // Ignore vertical component for "W" and "S"
        moveDirection.y = 0f;

        // Normalize the vectors
        moveDirection.Normalize();

        // Use CharacterController for better control over movement
        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up, mouseX);

        Camera mainCamera = Camera.main;

        Vector3 currentRotation = mainCamera.transform.rotation.eulerAngles;
        currentRotation.x -= mouseY;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -yRotationLimit, yRotationLimit);

        mainCamera.transform.rotation = Quaternion.Euler(currentRotation);
    }

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {
            if (crouchCoroutine != null)
                StopCoroutine(crouchCoroutine);

            crouchCoroutine = StartCoroutine(Crouch());
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            if (crouchCoroutine != null)
                StopCoroutine(crouchCoroutine);

            crouchCoroutine = StartCoroutine(UnCrouch());
        }
    }

    IEnumerator Crouch()
    {
        yield return new WaitForSeconds(crouchTransitionDelay);
        isCrouching = true;
        currentSpeed = crouchSpeed;
        transform.localScale = new Vector3(1f, crouchHeight, 1f);
    }

    IEnumerator UnCrouch()
    {
        yield return new WaitForSeconds(crouchTransitionDelay);
        isCrouching = false;
        currentSpeed = walkSpeed;
        transform.localScale = new Vector3(1f, standingHeight, 1f);
    }
}

