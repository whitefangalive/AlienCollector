using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVolume : MonoBehaviour
{
    private AudioSource AS;
    private float originalVolume;
    public SoundType type = SoundType.EFFECTS;

    public enum SoundType
    {
        AMBIENT,
        EFFECTS,
        CUSTOM
    }

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        if (AS != null )
        {
            originalVolume = AS.volume;
            switch (type)
            {
                case SoundType.AMBIENT:
                    AS.volume = originalVolume * PlayerPrefs.GetFloat("ambientVolume");
                    break;
                case SoundType.EFFECTS:
                    AS.volume = originalVolume * PlayerPrefs.GetFloat("effectsVolume");
                    break;
                default:
                    AS.volume = originalVolume;
                    break;
            }
        }
    }
    public void updateSound()
    {
        switch (type)
        {
            case SoundType.AMBIENT:
                AS.volume = originalVolume * PlayerPrefs.GetFloat("ambientVolume");
                break;
            case SoundType.EFFECTS:
                AS.volume = originalVolume * PlayerPrefs.GetFloat("effectsVolume");
                break;
            default:
                AS.volume = originalVolume;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (AS != null)
        {
            updateSound();
        }
    }
}
