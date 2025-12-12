using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource seSource;

    public float bgmVolume = 0.8f;
    public float seVolume = 0.8f;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SE_VOLUME_KEY = "SE_VOLUME";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume(); // Load saved volume
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadVolume()
    {
        bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.8f);
        seVolume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, 0.8f);
        
        bgmSource.volume = bgmVolume;
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        if (bgmSource.clip == clip) return; // Prevent restarting if same clip

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(AudioClip clip)
    {
        if (clip == null) return;
        seSource.PlayOneShot(clip, seVolume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public void SetSEVolume(float volume)
    {
        seVolume = volume;
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    // Optional: Fade out functionality can be added here if needed in future
}