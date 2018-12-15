using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    private float seekPosition;
    public AudioMixer mainMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource speechSource;
    public List<AudioClip> sounds;

    public static SoundController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("sfxVolume", volume);
    }

    public void SetSpeechVolume(float volume)
    {
        mainMixer.SetFloat("speechVolume", volume);
    }

    public void PlayBossSpeech()
    {
        speechSource.PlayOneShot(sounds[0]);
    }

    public void PlayEnemySpeech()
    {
        speechSource.PlayOneShot(sounds[1]);
    }

    public void PlayKratosFootsteps()
    {
        sfxSource.PlayOneShot(sounds[2]);
    }

    public void PlayEnemyFootsteps()
    {
        sfxSource.PlayOneShot(sounds[3]);
    }

    public void PlayKratosHit()
    {
        sfxSource.PlayOneShot(sounds[4]);
    }

    public void PlayEnemyHit()
    {
        sfxSource.PlayOneShot(sounds[5]);
    }

    public void PlayKratosDeath()
    {
        sfxSource.PlayOneShot(sounds[6]);
    }

    public void PlayEnemyDeath()
    {
        sfxSource.PlayOneShot(sounds[7]);
    }

    public void PlayCollectItem()
    {
        sfxSource.PlayOneShot(sounds[8]);
    }

    public void PlayRageActivation()
    {
        sfxSource.PlayOneShot(sounds[9]);
    }

    public void PlayMenuMusic()
    {
        PauseSounds();
        seekPosition = musicSource.time;
        musicSource.Stop();
        musicSource.clip = sounds[10];
        musicSource.time = 0;
        musicSource.Play();
    }

    public void PlayCalmMusic()
    {
        UnpauseSounds();
        musicSource.clip = sounds[11];
        if (!seekPosition.Equals(0))
        {
            musicSource.time = seekPosition;
            seekPosition = 0;
        }
        musicSource.Play();
    }

    public void PlayActionMusic()
    {
        UnpauseSounds();
        musicSource.clip = sounds[12];
        if (!seekPosition.Equals(0))
        {
            musicSource.time = seekPosition;
            seekPosition = 0;
        }
        musicSource.Play();
    }

    public void PauseSounds()
    {
        musicSource.Pause();
        sfxSource.Pause();
        speechSource.Pause();
    }

    public void UnpauseSounds()
    {
        sfxSource.UnPause();
        speechSource.UnPause();
    }

    public void Stop()
    {
        musicSource.Stop();
        sfxSource.Stop();
        speechSource.Stop();
    }
}
