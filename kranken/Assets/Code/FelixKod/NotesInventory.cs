using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInventory : Interact
{
    // Reference to the Inventory script
    public Inventory inventory;

    // Set the distance threshold for player proximity
    public float proximityDistance = 2.0f;

    void Start()
    {
        // Ensure that the required components are assigned
        if (inventory == null)
        {
            Debug.LogError("Inventory reference not set in NotesInventory script!");
        }
    }

    void Update()
    {
        // Check for player input to put a note into the inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Interact with the note
            Interactable();
        }
    }

    // Override the Interactable method from the base class
    public override void Interactable()
    {
        // Call the base Interactable method if it exists in the base class
        base.Interactable();

        // Check if the player is close enough to the note
        if (IsPlayerNearNote())
        {
            // Add the note to the inventory
            AddNoteToInventory();

            // Remove the note from the scene
            Destroy(gameObject);
        }
    }

    // Method to check if the player is near the note
    private bool IsPlayerNearNote()
    {
        // Calculate the distance between the player and the note
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Check if the player is within the proximity distance
        return distanceToPlayer <= proximityDistance;
    }

    // Method to add the note to the inventory
    private void AddNoteToInventory()
    {
        // Check if the SpriteRenderer component is present
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Example: Get information about the note (name, id, icon)
            string noteName = "Example Note";
            int noteID = 3; // Replace with the actual ID for notes
            Sprite noteIcon = spriteRenderer.sprite; // Use the SpriteRenderer component

            // Add the note to the inventory
            inventory.AddItem(noteName, noteID, noteIcon);

            // Optionally: You can also implement additional UI feedback or logic here
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the Note game object!");
        }
    }
}
