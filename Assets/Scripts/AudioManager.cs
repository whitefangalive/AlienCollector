using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void playAudioClip(AudioClip clip)
    {
        GameObject obj = new GameObject("AudioSource");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        newSource.volume = PlayerPrefs.GetFloat("effectsVolume");
        newSource.pitch = Random.Range(0.9f, 1.1f);
        newSource.playOnAwake = false;
        obj.AddComponent<DestroyOnAudioSourceEnd>();
        obj.AddComponent<dontDestroyOnLoad>();
        newSource.clip = clip;
        newSource.Play();

    }

    public static void playAudioClip(AudioClip clip, float pitch)
    {
        GameObject obj = new GameObject("AudioSource");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        newSource.volume = PlayerPrefs.GetFloat("effectsVolume");
        newSource.pitch = pitch;
        newSource.playOnAwake = false;
        obj.AddComponent<DestroyOnAudioSourceEnd>();
        obj.AddComponent<dontDestroyOnLoad>();
        newSource.clip = clip;
        newSource.Play();

    }


}
