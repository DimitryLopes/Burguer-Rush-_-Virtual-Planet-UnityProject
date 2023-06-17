using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioClip buttonClip;
    [SerializeField]
    private AudioClip countdownMusic;
    [SerializeField]
    private AudioClip mainGameMusic;
    [SerializeField]
    private AudioClip mainMenuMusic;

    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void PlayButtonAudio()
    {
        sfxSource.PlayOneShot(buttonClip);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            sfxSource.PlayOneShot(audioClip);
        }
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void PlayMenuMusic()
    {
        bgmSource.clip = mainMenuMusic;
        bgmSource.Play();
    }

    public void StopBGMMusic()
    {
        bgmSource.Stop();
    }

    public void PlayCountdownMusic()
    {
        bgmSource.clip = countdownMusic;
        bgmSource.Play();
    }

    public void PlayGameMusic()
    {
        bgmSource.clip = mainGameMusic;
        bgmSource.Play();
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
