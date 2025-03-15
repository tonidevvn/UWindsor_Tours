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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        musicSource.Play();
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
}
