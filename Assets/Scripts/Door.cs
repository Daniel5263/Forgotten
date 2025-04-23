using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot = 90;
    public float closeRot = 0;
    public float speed = 2;
    public bool opening;
    public AudioClip interactDoor;
    private AudioSource audioSource;

    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        if (opening)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
            }
        }
        else
        {
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
            }
        }
    }

    public void ToggleDoor()
    {
        opening = !opening;

        if (audioSource != null && interactDoor != null)
        {
            audioSource.PlayOneShot(interactDoor);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}