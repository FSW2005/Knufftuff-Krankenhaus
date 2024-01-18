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
}

