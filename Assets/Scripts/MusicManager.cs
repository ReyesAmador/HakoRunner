using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Header("Clips de música")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        PlayMenuMusic(); // Suena la música del menú al arrancar
    }

    void Update()
    {
        Debug.Log("🔊 Volumen actual audioSource: " + audioSource.volume);
    }

    public void PlayMenuMusic()
    {
        if (audioSource.clip != menuMusic)
        {
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (audioSource.clip != gameplayMusic)
        {
            audioSource.clip = gameplayMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
