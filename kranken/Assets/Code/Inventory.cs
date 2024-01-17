using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Strukturen f�r Items
    [System.Serializable]
    public struct Item
    {
        public string itemName;
        public int itemID;
        public Sprite itemIcon;
        // Adda nya properties f�r necces�ra Items
    }

    // List: Inventory Items
    private List<Item> inventory = new List<Item>();

    // Test UI f�r inventory display 
    public GameObject inventoryUIPrefab;
    private GameObject inventoryUI;

    void Start()
    {
        // Exempel: Adda initiala Items till inventory
      // -----------------------  AddItem("Sword", 1, swordIcon);
     //   ---------------------- AddItem("Potion", 2, potionIcon);

        // Exempel: Display inventory UI
        ShowInventoryUI();
    }

    void Update()
    {
        // Exempel: �ppna/st�ng inventory genom key tryck "I" 
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    // Adda Item till inventory
    public void AddItem(string name, int id, Sprite icon)
    {
        Item newItem = new Item
        {
            itemName = name,
            itemID = id,
            itemIcon = icon
        };

        inventory.Add(newItem);

        // Updatera UI f�r eventuell implementering
        UpdateInventoryUI();
    }

    // Kasta bort items fr�n inventory genom ID
    public void RemoveItem(int id)
    {
        inventory.RemoveAll(item => item.itemID == id);

        // Updatera UI f�r eventuell implementering
        UpdateInventoryUI();
    }

    // Updatera UI till nuvarande 
    void UpdateInventoryUI()
    {
        // Implementera UI updateringar baserad p� nuvarande stadiet inventory beffiner sig i
        // M�jligen: Updatera UI panel, adda/ta bort UI element, etc.
        // Kan anv�nda 'inventory' listan f�r att komma �t nuvarande Items i inventory.
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

    // St�ng ner inventory UI
    void CloseInventoryUI()
    {
        if (inventoryUI != null)
        {
            Destroy(inventoryUI);
        }
    }

    // Toggla inventory UI visibility
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
