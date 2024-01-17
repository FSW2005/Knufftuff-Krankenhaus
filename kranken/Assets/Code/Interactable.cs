using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public string itemName = "DefaultItem"; // Unique identifier for the item
    public AudioClip pickupSound; // Sound played when the item is picked up
    public string sceneToOpen; // Name of the scene to open (make sure it's added to the build settings)

    void Update()
    {
        // Check for player input (e.g., pressing the "E" key) to trigger interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast to detect the interactable object in front of the player
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f))
            {
                Interact(hit.collider.gameObject);
            }
        }
    }

    void Interact(GameObject interactingObject)
    {
        // Check if the interacting object is the player
        if (interactingObject.CompareTag("Player"))
        {
            // Play pickup sound if available
            if (pickupSound != null)
            {
                AudioManager.instance.PlaySoundEffect(pickupSound);
            }

            // Check if there is a scene to open
            if (!string.IsNullOrEmpty(sceneToOpen))
            {
                // Load the specified scene
                SceneManager.LoadScene(sceneToOpen);
            }
        }
    }
}
