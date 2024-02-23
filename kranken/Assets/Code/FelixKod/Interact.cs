using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Metoden blir kallad vid en interaktion med ett objekt
    public virtual void Interactable()
    {
        Debug.Log("Interacting with: " + gameObject.name);
        // Adda logic nedan
    }

    void Update()
    {
        // Spelar input (e.g., trycka "E" tangenten) för interaktion
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast använd för att leta efter interegerbara objekt
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f))
            {
                Interact interactable = hit.collider.GetComponent<Interact>();
                if (interactable != null)
                {
                    // Kalla denna metod efter interaktion med "interactable object"
                    interactable.Interactable();
                }
            }
        }
    }
}

