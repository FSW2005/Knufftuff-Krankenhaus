using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Define a structure for items
    [System.Serializable]
    public struct Item
    {
        public string itemName;
        public int itemID;
        public Sprite itemIcon;
        // Add any other properties you need for your items
    }

    // List to store the inventory items
    private List<Item> inventory = new List<Item>();

    // Example UI for inventory display (you may have a more complex UI in your actual project)
    public GameObject inventoryUIPrefab;
    private GameObject inventoryUI;

    void Start()
    {
        // Example: Add some initial items to the inventory
      // -----------------------  AddItem("Sword", 1, swordIcon);
     //   ---------------------- AddItem("Potion", 2, potionIcon);

        // Example: Display the inventory UI
        ShowInventoryUI();
    }

    void Update()
    {
        // Example: Open/Close inventory on "I" key press
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    // Add item to the inventory
    public void AddItem(string name, int id, Sprite icon)
    {
        Item newItem = new Item
        {
            itemName = name,
            itemID = id,
            itemIcon = icon
        };

        inventory.Add(newItem);

        // You may want to update UI here to reflect the change
        UpdateInventoryUI();
    }

    // Remove item from the inventory by ID
    public void RemoveItem(int id)
    {
        inventory.RemoveAll(item => item.itemID == id);

        // You may want to update UI here to reflect the change
        UpdateInventoryUI();
    }

    // Update the UI to reflect the current inventory
    void UpdateInventoryUI()
    {
        // Implement UI updates based on the current state of the inventory
        // This could involve updating a UI panel, adding/removing UI elements, etc.
        // You can use the 'inventory' list to access the current items in the inventory.
    }

    // Display the inventory UI
    void ShowInventoryUI()
    {
        if (inventoryUIPrefab != null)
        {
            inventoryUI = Instantiate(inventoryUIPrefab, Vector3.zero, Quaternion.identity);
            UpdateInventoryUI();
        }
    }

    // Close the inventory UI
    void CloseInventoryUI()
    {
        if (inventoryUI != null)
        {
            Destroy(inventoryUI);
        }
    }

    // Toggle the inventory UI visibility
    void ToggleInventoryUI()
    {
        if (inventoryUI == null)
        {
            ShowInventoryUI();
        }
        else
        {
            CloseInventoryUI();
        }
    }
}
