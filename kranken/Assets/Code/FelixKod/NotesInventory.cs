using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInventory : Interact
{
    // Referencerar Inventory script
    public Inventory inventory;

    // Distans threshold för spelar proximity
    public float proximityDistance = 2.0f;

    void Start()
    {
        // Säkrar kraven för funktionen
        if (inventory == null)
        {
            Debug.LogError("Inventory reference not set in NotesInventory script!");
        }
    }

    void Update()
    {
        // Om spelaren trycker på (E) läggs objektet in i Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Interegera med papperslappen
            Interactable();
        }
    }

    // Override the Interactable method from the base class
    public override void Interactable()
    {
        base.Interactable();

        // Nära nog till objektet
        if (IsPlayerNearNote())
        {
            // Adda objektet till inventory
            AddNoteToInventory();

            // Tar objektet bort frpn scenen
            Destroy(gameObject);
        }
    }

    // Nära nog till objektet
    private bool IsPlayerNearNote()
    {
        // Distansen mellan objektet och spelaren kalkuleras
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        // proximity distance
        return distanceToPlayer <= proximityDistance;
    }

    // addar objektet till inventory
    private void AddNoteToInventory()
    {
        // Kollar om SpriteRenderer component är tilljänlig
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // (name, id, icon)
            string noteName = "Example Note";
            int noteID = 3; // (Replace) 
            Sprite noteIcon = spriteRenderer.sprite; // adda SpriteRenderer component

            // note --> inventory
            inventory.AddItem(noteName, noteID, noteIcon);

            // additional UI feedback eller logik nedan
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the Note game object!");
        }
    }
}
