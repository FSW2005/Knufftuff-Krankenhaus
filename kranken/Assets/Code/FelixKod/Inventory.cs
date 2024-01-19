using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Structure for items
    [System.Serializable]
    public struct Item
    {
        public string itemName;
        public int itemID;
        public Sprite itemIcon;
        // Add other necessary item properties
    }

    // List: Inventory Items
    private List<Item> inventory = new List<Item>();

    // Test UI for inventory display 
    public GameObject inventoryUIPrefab;
    private GameObject inventoryUI;

    void Start()
    {
        // Example: Add initial items to the inventory
        // AddItem("Sword", 1, swordIcon);
        // AddItem("Potion", 2, potionIcon);

        // Example: Display inventory UI
        ShowInventoryUI();
    }

    void Update()
    {
        // Example: Open/close inventory with the "I" key 
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

        // Update UI if necessary
        UpdateInventoryUI();
    }

    // Remove items from the inventory by ID
    public void RemoveItem(int id)
    {
        inventory.RemoveAll(item => item.itemID == id);

        // Update UI if necessary
        UpdateInventoryUI();
    }

    // Update UI to current state
    void UpdateInventoryUI()
    {
        // Implement UI updates based on the current state of the inventory
        // For example: Update UI panel, add/remove UI elements, etc.
        // You can use the 'inventory' list to access current items in the inventory.
    }

    // Show inventory UI
    void ShowInventoryUI()
    {
        if (inventoryUIPrefab != null)
        {
            inventoryUI = Instantiate(inventoryUIPrefab, Vector3.zero, Quaternion.identity);
            UpdateInventoryUI();
        }
    }

    // Close inventory UI
    void CloseInventoryUI()
    {
        if (inventoryUI != null)
        {
            Destroy(inventoryUI);
        }
    }

    // Toggle inventory UI visibility
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
