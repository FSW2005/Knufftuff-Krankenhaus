using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interact
{
    private Animator doorAnimator;
    private bool isOpen = false;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private AudioSource audioSource;

    // Variabel som h�ller koll p� om d�rren h�ller p� att �pnas eller st�ngas
    private bool isAnimating = false;

    private Quaternion originalRotation; 
    private BoxCollider doorCollider; 

    void Start()
    {
        // Hittar animations spelaren p� d�rren (finns inte)
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door. Add an Animator component.");
        }

        // Kan adda en ljudfil till d�rren
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // Use 3D spatial audio

        // Sparar orginala rotationen av d�rren
        originalRotation = transform.rotation;

        // V�ljer BoxCollider komponent av d�ren
        doorCollider = GetComponent<BoxCollider>();
        if (doorCollider == null)
        {
            Debug.LogError("BoxCollider component not found on the door. Add a BoxCollider component.");
        }
    }

    // Applicerar "Interactable" p� d�ren s� spelaren kan interegera med d�rren, �ppna/st�nga.
    public override void Interactable()
    {
        base.Interactable();

        // Stoppar spelaren fr�n att interigera med d�rren under animation spelandet
        if (isAnimating)
        {
            return;
        }

        // �pen/St�ngd d�r
        isOpen = !isOpen;

        // Aktivera animation (existerar inte, welp)
        doorAnimator.SetBool("IsOpen", isOpen);

        // Spela respektive ansatta audiofil
        if (isOpen && doorOpenSound != null)
        {
            PlayDoorSound(doorOpenSound);
        }
        else if (!isOpen && doorCloseSound != null)
        {
            PlayDoorSound(doorCloseSound);
        }

        // Rotera d�rren f�r vid interaktion 90 grader
        if (isOpen && !isAnimating)
        {
            StartCoroutine(RotateDoor(90f));
            // BoxCollider inaktiveras d� d�ren �ppnas
            doorCollider.enabled = false;
        }
        else if (!isOpen && !isAnimating)
        {
            // D�rren �terv�nder til orginalposition efter �nnu en interaktion 
            StartCoroutine(RotateDoor(0f));
            // BoxCollider aktiveras d� d�rren st�ngs
            doorCollider.enabled = true;
        }
    }

    // Spelar en ljudfil d� d�rren �ppnas 
    void PlayDoorSound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine(AnimationDelay());
        }
    }

    // Koden f�r v�nta?
    IEnumerator AnimationDelay()
    {
        isAnimating = true;
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);
        isAnimating = false;
    }

    // Kod f�r att rotera d�rren 
    IEnumerator RotateDoor(float targetAngle)
    {
        float duration = 1.0f; // Adjust the rotation duration as needed
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, targetAngle, 90);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
