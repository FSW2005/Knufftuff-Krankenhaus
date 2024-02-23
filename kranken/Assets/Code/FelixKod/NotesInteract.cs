using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInteract : Interact
{
    // properties specific to the Notes script can be added here

    // Material vid highlighted
    public Material highlightMaterial;

    // Original material av papperslapp objectet
    private Material originalMaterial;

    // Audiofil att spela vid interaktion med en papperslapp 
    public AudioClip interactAudioClip;

    // Reference AudioSource 
    private AudioSource audioSource;

    // R�ckvid f�r interaktion 
    public float proximityDistance = 2.0f;

    void Start()
    {
        // Adda Audiofil till GameObjectet (finns ej)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = interactAudioClip;

        audioSource.playOnAwake = false;

        // Spara originala materialet av note object
        originalMaterial = GetComponent<Renderer>().material;
    }

    // Override Interactable method fr�n bas classen
    public override void Interactable()
    {
        // Framkalla base Interactable method
        base.Interactable();

        // Spela interaction audio 
        PlayInteractionAudio();

        // Kollar spelarens r�ckvid till papperslappen
        if (IsPlayerNearNote())
        {
            // Adda papperslappen till en inventory
            AddNoteToInventory();

            // Tar bort note objektet efter interaktion
            Destroy(gameObject);
        }
    }

    // Method som spelar audio vid interaktion
    private void PlayInteractionAudio()
    {
        // Test: audio tillsat till objekt?
        if (interactAudioClip != null)
        {
            // Spela audiofilen
            audioSource.Play();
        }
    }

    // Kollar om spelaren �r n�ra nog till objektet
    private bool IsPlayerNearNote()
    {
        // Distansen mellan spelaren och objektet kalkuleras
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Kollar om spelaren st�r tillr�ckligt n�ra objektet f�r en interaktion
        return distanceToPlayer <= proximityDistance;
    }

    // adda objektet till inventory
    private void AddNoteToInventory()
    {
        // Inventory script  till spelaren (disfunktionell)
        Inventory inventory = FindObjectOfType<Inventory>();
        
        if (inventory != null)
        {
            string noteName = "Example Note";
            int noteID = 3; // (Replace) 
            Sprite noteIcon = GetComponent<SpriteRenderer>().sprite; // (Replace)

            // Adda objektet �t inventory
            inventory.AddItem(noteName, noteID, noteIcon);
        }
        else
        {
            Debug.LogError("Inventory script not found!");
        }
    }

    // Highlight funktion
    void Update()
    {
        // Kollar om spelare �r n�ra not objektet
        if (IsPlayerNearNote())
        {
            // �ndrar materialet om spelaren �r det
            GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            // Om inte sker ingen �ndring med materialet
            GetComponent<Renderer>().material = originalMaterial;
        }
        {
            // Kollar om spelare �r n�ra not objektet
            if (IsPlayerNearNote())
            {
                // �ndrar materialet om spelaren �r det
                Debug.Log("Highlighting");
                GetComponent<Renderer>().material = highlightMaterial;
            }
            else
            {
                // Om inte sker ingen �ndring med materialet
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }
}
