using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
// Bossar allt ljud inom spelet, kolt va?
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;
    public AudioSource[] soundEffects;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource availableSource = GetAvailableSoundEffectSource();
        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.Play();
        }
    }

    AudioSource GetAvailableSoundEffectSource()
    {
        foreach (AudioSource audioSource in soundEffects)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        return null;
    }
}
    