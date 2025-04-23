using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickClip;

    private AudioSource _audioSource;
    private Button _button;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClick);
    }

    void PlayClick()
    {
        if (clickClip != null)
            _audioSource.PlayOneShot(clickClip);
    }
}
