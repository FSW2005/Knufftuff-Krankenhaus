using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Item structure
    [System.Serializable]
    public struct Item
    {
        public string itemName;
        public int itemID;
        public Sprite itemIcon;
        // Properties ev. addade
    }

    // List: Inventory Items
    private List<Item> inventory = new List<Item>();

    // Testa UI för inventory display 
    public GameObject inventoryUIPrefab;
    private GameObject inventoryUI;

    void Start()
    {
        // Adda items som ska in i inventory

        // Display inventory UI (Inte gjord)
        ShowInventoryUI();
    }

    void Update()
    {
        // Öppna/stäng inventory med tangenten "I" 
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    // Adda item till inventory
    public void AddItem(string name, int id, Sprite icon)
    {
        Item newItem = new Item
        {
            itemName = name,
            itemID = id,
            itemIcon = icon
        };

        inventory.Add(newItem);

        // Uppdaterar UI:n  
        UpdateInventoryUI();
    }

    // Remove items from the inventory by ID
    public void RemoveItem(int id)
    {
        inventory.RemoveAll(item => item.itemID == id);

        // Uppdaterar UI:n  
        UpdateInventoryUI();
    }

    // Updatera UI:n till nytt stadie
    void UpdateInventoryUI()
    {
        // Implementera UI updates (baserat på inventoryiets stadie)
    }

    // Visa inventory UI
    void ShowInventoryUI()
    {
        if (inventoryUIPrefab != null)
        {
            inventoryUI = Instantiate(inventoryUIPrefab, Vector3.zero, Quaternion.identity);
            UpdateInventoryUI();
        }
    }

    // Stäng inventory UI
    void CloseInventoryUI()
    {
        if (inventoryUI != null)
        {
            Destroy(inventoryUI);
        }
    }

    // Sätt på/av inventory UI visibility
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
