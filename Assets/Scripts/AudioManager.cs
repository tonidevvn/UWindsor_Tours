using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Sources ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    [Header("--- Audio Clips ---")]
    public AudioClip[] backgroundMusic;
    public AudioClip[] walkingSound;
    public AudioClip[] runningSound;
    public AudioClip[] jumpingStartSound;
    public AudioClip[] jumpingLandSound;

    [Header("--- UI Sounds ---")]
    public AudioClip[] mouseClickSound;
    public AudioClip[] mouseHoverSound;

    private const string MUSIC_VOL_KEY = "musicVolume";
    private const string SFX_VOL_KEY = "sfxVolume";

    public void LoadVolumeSettings()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 0.5f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_VOL_KEY, 0.5f);

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, GetMusicVolume());
        PlayerPrefs.SetFloat(SFX_VOL_KEY, GetSFXVolume());
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource != null ? musicSource.volume : 0.5f;
    }

    public float GetSFXVolume()
    {
        return sfxSource != null ? sfxSource.volume : 0.5f;
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null) {
            musicSource.volume = value;
            Debug.Log($"[AudioManager] Music volume set to {value}");
        }
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSource != null) {            
            sfxSource.volume = value;
            Debug.Log($"[AudioManager] SFX volume set to {value}");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        musicSource.Play();
    }

    void Awake()
    {        
        LoadVolumeSettings();
    }

    // Update is called once per frame
    public void PlayWalking()
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(walkingSound[Random.Range(0, walkingSound.Length)]);
        }
    }

    public void PlayRunning()
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(runningSound[Random.Range(0, runningSound.Length)]);
        }
    }
    public void PlayJumpingStart()
    {
        sfxSource.PlayOneShot(jumpingLandSound[Random.Range(0, jumpingLandSound.Length)]);
    }
    public void PlayJumpingLand()
    {
        sfxSource.PlayOneShot(jumpingLandSound[Random.Range(0, jumpingLandSound.Length)]);
    }

    public void PlayMouseClick()
    {
        sfxSource.PlayOneShot(mouseClickSound[Random.Range(0, mouseClickSound.Length)]);
    }
    public void PlayMouseHover()
    {
        sfxSource.PlayOneShot(mouseHoverSound[Random.Range(0, mouseHoverSound.Length)]);
    }
}
