using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    private float speechVolume;
    private float sfxVolume;
    private float musicVolume;
    private int currentMusicIndex;
    public AudioSource source;
    public AudioClip[] musicClips;

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

    public void Start()
    {
        PlayMusic(0);
    }

    public void PauseMusic()
    {
        source.Pause();
    }

    public void ResumeMusic()
    {
        source.UnPause();
    }

    public void PlayMusic(int index)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        currentMusicIndex = index % musicClips.Length;
        source.clip = musicClips[currentMusicIndex];
        source.Play();
    }
}
