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
    public AudioClip bossLaser;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
}
