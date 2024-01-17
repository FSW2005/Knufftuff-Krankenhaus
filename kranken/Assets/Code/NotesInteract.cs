using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInteract : Interact
{
    // Additional properties specific to the Notes script can be added here

    // Override the Interactable method from the base class
    public override void Interactable()
    {
        // Call the base Interactable method
        base.Interactable();

        // Check if the player is close enough to the note
        if (IsPlayerNearNote())
        {
            // Add the note to the inventory
            AddNoteToInventory();

            // Optionally: Remove the note from the scene or handle any other logic
            Destroy(gameObject);
        }
    }

    // Method to check if the player is near the note
    private bool IsPlayerNearNote()
    {
        // Set the distance threshold for player proximity
        float proximityDistance = 2.0f;

        // Calculate the distance between the player and the note
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Check if the player is within the proximity distance
        return distanceToPlayer <= proximityDistance;
    }

    // Method to add the note to the inventory
    private void AddNoteToInventory()
    {
        // Get the Inventory script attached to the player or wherever it is located
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory != null)
        {
            // Example: Get information about the note (name, id, icon)
            string noteName = "Example Note";
            int noteID = 3; // Replace with the actual ID for notes
            Sprite noteIcon = GetComponent<SpriteRenderer>().sprite; // Replace with the actual way to get the note's icon

            // Add the note to the inventory
            inventory.AddItem(noteName, noteID, noteIcon);

            // Optionally: You can also implement additional UI feedback or logic here
        }
        else
        {
            Debug.LogError("Inventory script not found!");
        }
    }
}