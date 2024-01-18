using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInventory : Inventory
{
    // Reference to the Inventory script
    public Inventory inventory;

    // Reference to the Notes script
    public NotesInventory notes;

    void Start()
    {
        // Ensure that the required components are assigned
        if (inventory == null)
        {
            Debug.LogError("Inventory reference not set in NotesInventory script!");
        }

        if (notes == null)
        {
            Debug.LogError("Notes reference not set in NotesInventory script!");
        }
    }

    void Update()
    {
        // Example: Check for player input to put a note into the inventory
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNoteToInventory();
        }
    }

    // Method to add a note to the inventory
    void AddNoteToInventory()
    {
        // Example: Check if the player is near a note (customize this logic based on your game)
        if (IsPlayerNearNote())
        {
            // Example: Get information about the note (name, id, icon)
            string noteName = "Example Note";
            int noteID = 3; // Replace with the actual ID for notes
            Sprite noteIcon = notes.GetComponent<SpriteRenderer>().sprite; // Replace with the actual way to get the note's icon

            // Add note to the inventory
            inventory.AddItem(noteName, noteID, noteIcon);

            // Optionally: Remove the note from the scene or handle any other logic
            Destroy(notes.gameObject);
        }
    }

    // Example method to check if the player is near a note (customize based on your game)
    bool IsPlayerNearNote()
    {
        // Replace with own logic to determine if the player is near a note
        // raycasting, trigger zones, or any other method possible.
        return true; // Placeholder, replace with actual logic
    }
}