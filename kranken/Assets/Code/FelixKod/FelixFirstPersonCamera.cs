using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FelixFirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 2f;
    public float yRotationLimit = 88f;

    private Vector2 rotation = Vector2.zero;

    void Start()
    {
        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotation();
    }

    public void UpdateRotation()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}