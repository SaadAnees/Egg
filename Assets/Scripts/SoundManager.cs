using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Crack Sounds")]
    public AudioClip[] eggCrackSounds; // Assign multiple cracking clips

    [Header("Other Sounds")]
    public AudioClip dragonRoar;
    public AudioClip click;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayRandomCrack()
    {
        if (eggCrackSounds.Length > 0)
        {
            int index = Random.Range(0, eggCrackSounds.Length);
            PlaySound(eggCrackSounds[index]);
        }
    }

    public void PlayRoar() => PlaySound(dragonRoar);
    public void PlayClick() => PlaySound(click);
}
