using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{ 
    public static AudioManager Instance;

    [Header("AudioSources")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("music AudioClips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip bossMusic;

    [Header("SFX AudioClips")]
    public AudioClip duifSound;
    public AudioClip ratSound;
    public AudioClip fishSound;
    public AudioClip catGettingHit;
    public AudioClip catShooting;
    public AudioClip bossFightStart;
    public AudioClip droneShooting;
    public AudioClip laserCharging;
    public AudioClip frontlaserShooting;
    public AudioClip uplaserShooting;
    public AudioClip dashAttack;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;// de reference naar het script
        }
        else
        {
            Destroy(gameObject); // zorg ervoor dat er altijd maar 1 is
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSoundEffect()
    {
        SFXSource.Stop();
    }
}
