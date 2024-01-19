using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interact
{
    private Animator doorAnimator;
    private bool isOpen = false;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private AudioSource audioSource;

    // Variable to track if the door is currently in the process of opening or closing
    private bool isAnimating = false;

    private Quaternion originalRotation; // Store the original rotation of the door
    private BoxCollider doorCollider; // Reference to the BoxCollider component

    void Start()
    {
        // Find the Animator component on the door
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door. Add an Animator component.");
        }

        // Add an AudioSource component to the door GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // Use 3D spatial audio

        // Store the original rotation of the door
        originalRotation = transform.rotation;

        // Get the BoxCollider component on the door
        doorCollider = GetComponent<BoxCollider>();
        if (doorCollider == null)
        {
            Debug.LogError("BoxCollider component not found on the door. Add a BoxCollider component.");
        }
    }

    // Override the Interact method to open/close the door
    public override void Interactable()
    {
        base.Interactable();

        // Check if the door is currently animating (opening or closing)
        if (isAnimating)
        {
            return;
        }

        // Toggle the state of the door (open/close)
        isOpen = !isOpen;

        // Trigger the corresponding animation
        doorAnimator.SetBool("IsOpen", isOpen);

        // Play the corresponding audio clip
        if (isOpen && doorOpenSound != null)
        {
            PlayDoorSound(doorOpenSound);
        }
        else if (!isOpen && doorCloseSound != null)
        {
            PlayDoorSound(doorCloseSound);
        }

        // Rotate the door by 90 degrees on the Y-axis after the first interaction
        if (isOpen && !isAnimating)
        {
            StartCoroutine(RotateDoor(90f));
            // Disable the BoxCollider when the door is open
            doorCollider.enabled = false;
        }
        else if (!isOpen && !isAnimating)
        {
            // Reset the door to its original rotation on the second interaction
            StartCoroutine(RotateDoor(0f));
            // Enable the BoxCollider when the door is closed
            doorCollider.enabled = true;
        }
    }

    // Play the door sound with a delay to match the animation duration
    void PlayDoorSound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine(AnimationDelay());
        }
    }

    // Coroutine to wait for the animation to finish before allowing another interaction
    IEnumerator AnimationDelay()
    {
        isAnimating = true;
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);
        isAnimating = false;
    }

    // Coroutine to rotate the door smoothly
    IEnumerator RotateDoor(float targetAngle)
    {
        float duration = 1.0f; // Adjust the rotation duration as needed
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, targetAngle, 90);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
