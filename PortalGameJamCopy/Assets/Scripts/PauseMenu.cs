using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(AudioSettingsManager.instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioSettingsManager.instance.SetSFXVolume);   
    }
}
