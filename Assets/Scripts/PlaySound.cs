using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public float pitch = 1f;
    public float volume = 1f;
    public enum SoundType
    {
        AMBIENT,
        EFFECTS,
        CUSTOM
    }

    public SoundType type = SoundType.EFFECTS;
    public void playAudioClip(AudioClip clip)
    {
        GameObject obj = new GameObject("AudioSource");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        switch (type)
        {
            case SoundType.AMBIENT:
                newSource.volume = PlayerPrefs.GetFloat("ambientVolume");
                break;
            case SoundType.EFFECTS:
                newSource.volume = PlayerPrefs.GetFloat("effectsVolume");
                break;
            default:
                newSource.volume = volume;
                break;
        }
        
        newSource.playOnAwake = false;
        obj.AddComponent<DestroyOnAudioSourceEnd>();
        obj.AddComponent<dontDestroyOnLoad>();
        newSource.pitch = pitch;
        newSource.clip = clip;
        newSource.Play();
    }
}
