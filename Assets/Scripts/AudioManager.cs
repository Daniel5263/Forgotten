using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource footstepSource;

    public AudioClip harpSound;
    public AudioClip closeDoorCreaking;
    public AudioClip creepyDistortion;
    public AudioClip farDoorCreaking;
    public AudioClip pianoJump;
    public AudioClip pickupNote;
    public AudioClip playerFootsteps;
    public AudioClip screamerJumpscare;
    public AudioClip staticFollow;
    public AudioClip whisperingWinds;
    public AudioClip flashlight;

    void Start()
    {
        if (musicSource != null && harpSound != null)
        {
            musicSource.loop = true;
            musicSource.clip = harpSound;
            musicSource.Play();
        }
    }

    public void PlayCloseDoorCreak()
    {
        if (sfxSource != null && closeDoorCreaking != null)
        {
            sfxSource.PlayOneShot(closeDoorCreaking);
        }
    }

    public void PlayPickupNote()
    {
        if (sfxSource != null && pickupNote != null)
        {
            sfxSource.PlayOneShot(pickupNote);
        }
    }

    public void PlayPianoJump()
    {
        if (sfxSource != null && pianoJump != null)
        {
            sfxSource.PlayOneShot(pianoJump);
        }
    }


}
