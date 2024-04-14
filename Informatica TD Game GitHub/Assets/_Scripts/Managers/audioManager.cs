using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource EnemiesDeathSource;
    [SerializeField] private AudioSource ShootingSource;
    [SerializeField] private AudioSource SpeakerSource;
    [SerializeField] private AudioSource CheckoutDiscountSource;
    
    

    [Header("AudioClip")] 
    public AudioClip background;
    public AudioClip death;
    public AudioClip pew;
    public AudioClip bass;
    public AudioClip ching;
    

    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayEnemiesDeath(AudioClip clip)
    {
        EnemiesDeathSource.PlayOneShot(clip);
    }
    
    public void PlayPew(AudioClip clip)
    {
        ShootingSource.PlayOneShot(clip);
    }

    
    public void PlayCheckoutDiscount(AudioClip clip)
    {
        CheckoutDiscountSource.PlayOneShot(clip);
    }
    
    public void PlaySpeaker(AudioClip clip)
    {
        SpeakerSource.PlayOneShot(clip);
    }
    
}
