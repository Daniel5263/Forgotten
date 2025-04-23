using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundOnInteract : MonoBehaviour
{
    public AudioClip interactClip;

    public GameObject promptUI;

    public KeyCode interactKey = KeyCode.E;

    private AudioSource audioSource;
    private bool playerInRange = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (promptUI)
            promptUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            playerInRange = true;
            if (promptUI)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            playerInRange = false;
            if (promptUI)
                promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (interactClip != null)
                audioSource.PlayOneShot(interactClip);
        }
    }
}
