using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public AudioSource carMovingSound;
    public AudioSource carStoppingSound;
    public AudioSource carReversingSound;

    private float masterVolume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

       // musicSource = gameObject.AddComponent<AudioSource>();
        //sfxSource = gameObject.AddComponent<AudioSource>();

       // PlayBackgroundMusic();
    }

    public void Click() 
    {
        PlaySoundEffect(soundEffects[0]);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);

        musicSource.volume = masterVolume;
        sfxSource.volume = masterVolume;
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, masterVolume);
    }
    public void StopSoundEffect() 
    {
        sfxSource.Stop();
    }

    public void Pause()
    {
        musicSource.Pause();
        sfxSource.Pause();
    }

    public void Resume()
    {
        musicSource.UnPause();
        sfxSource.UnPause();
    }

    // Add more methods as needed
}
