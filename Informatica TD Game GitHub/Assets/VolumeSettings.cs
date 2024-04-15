using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider EnemiesDeathSlider;
    [SerializeField] private Slider TowersSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        

        else
        {
            SetMusicVolume();
            SetEnemiesDeathVolume();
            SetTowerVolume();
        }
    }
        

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetEnemiesDeathVolume()
    {
        float volume = EnemiesDeathSlider.value;
        myMixer.SetFloat("Enemies", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("EnemiesVolume", volume);
    }
    
    public void SetTowerVolume()
    {
        float volume = TowersSlider.value;
        myMixer.SetFloat("Towers", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("TowersVolume", volume);
    }


    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        EnemiesDeathSlider.value = PlayerPrefs.GetFloat("EnemiesVolume");
        TowersSlider.value = PlayerPrefs.GetFloat("TowersVolume");

        SetEnemiesDeathVolume();
        SetMusicVolume();
        SetTowerVolume();

    }
    

}
