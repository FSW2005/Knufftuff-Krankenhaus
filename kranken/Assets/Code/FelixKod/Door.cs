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

    // Variabel som håller koll på om dörren håller på att öpnas eller stängas
    private bool isAnimating = false;

    private Quaternion originalRotation; 
    private BoxCollider doorCollider; 

    void Start()
    {
        // Hittar animations spelaren på dörren (finns inte)
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door. Add an Animator component.");
        }

        // Kan adda en ljudfil till dörren
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // Use 3D spatial audio

        // Sparar orginala rotationen av dörren
        originalRotation = transform.rotation;

        // Väljer BoxCollider komponent av dören
        doorCollider = GetComponent<BoxCollider>();
        if (doorCollider == null)
        {
            Debug.LogError("BoxCollider component not found on the door. Add a BoxCollider component.");
        }
    }

    // Applicerar "Interactable" på dören så spelaren kan interegera med dörren, öppna/stänga.
    public override void Interactable()
    {
        base.Interactable();

        // Stoppar spelaren från att interigera med dörren under animation spelandet
        if (isAnimating)
        {
            return;
        }

        // Öpen/Stängd dör
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

        // Rotera dörren för vid interaktion 90 grader
        if (isOpen && !isAnimating)
        {
            StartCoroutine(RotateDoor(90f));
            // BoxCollider inaktiveras då dören öppnas
            doorCollider.enabled = false;
        }
        else if (!isOpen && !isAnimating)
        {
            // Dörren återvänder til orginalposition efter ännu en interaktion 
            StartCoroutine(RotateDoor(0f));
            // BoxCollider aktiveras då dörren stängs
            doorCollider.enabled = true;
        }
    }

    // Spelar en ljudfil då dörren öppnas 
    void PlayDoorSound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine(AnimationDelay());
        }
    }

    // Koden får vänta?
    IEnumerator AnimationDelay()
    {
        isAnimating = true;
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);
        isAnimating = false;
    }

    // Kod för att rotera dörren 
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
