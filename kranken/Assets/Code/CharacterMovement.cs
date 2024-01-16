using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input handling for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Rotate towards movement direction
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.deltaTime));
        }

        // Move the player
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime * moveDirection.magnitude;
        rb.MovePosition(transform.position + movement);
    }
}
