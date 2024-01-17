using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interact
{
    private Animator doorAnimator;
    private bool isOpen = false;

    void Start()
    {
        // Find the Animator component on the door
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door. Add an Animator component.");
        }
    }

    // Override the Interact method to open/close the door
    public override void Interactable()
    {
        base.Interactable();

        // Toggle the state of the door (open/close)
        isOpen = !isOpen;

        // Trigger the corresponding animation
        doorAnimator.SetBool("IsOpen", isOpen);
    }
}
