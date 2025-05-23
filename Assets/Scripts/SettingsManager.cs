using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Toggle Accelerometer;
    private bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
         Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        if (loaded)
        {
            Save();
        }
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        } 
        else
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
        }

        if (PlayerPrefs.HasKey("ambientVolume"))
        {
            ambientSlider.value = PlayerPrefs.GetFloat("ambientVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("ambientVolume", 1);
        }

        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
        } 
        else
        {
            PlayerPrefs.SetFloat("effectsVolume", 1);
        }
        if (PlayerPrefs.HasKey("accelerometer"))
        {
            Accelerometer.isOn = PlayerPrefs.GetInt("accelerometer") != 0;
        } else
        {
            PlayerPrefs.SetInt("accelerometer", 0);
        }
        
        loaded = true;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("ambientVolume", ambientSlider.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        PlayerPrefs.SetInt("accelerometer", Convert.ToInt32(Accelerometer.isOn));
    }
}
