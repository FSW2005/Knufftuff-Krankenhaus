using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // This method will be called when the player interacts with the object
    public virtual void Interactable()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        // Add interaction logic here
    }

    void Update()
    {
        // Check for player input (e.g., pressing the "E" key) to trigger interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast to detect the interactable object in front of the player
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f))
            {
                Interact interactable = hit.collider.GetComponent<Interact>();
                if (interactable != null)
                {
                    // Call the Interact method of the interactable object
                    interactable.Interactable();
                }
            }
        }
    }
}

