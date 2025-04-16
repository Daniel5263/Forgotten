using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public GameObject interactText;     // UI text showing "Press E to open/close"
    public DoorController doorController;

    private bool inReach = false;

    void Start()
    {
        // Hide interaction text at start
        if (interactText != null)
            interactText.SetActive(false);
    }

    // Trigger Enter: when the player's "Reach" collider enters the door's trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    // Trigger Exit: player’s "Reach" collider leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    void Update()
    {
        // If the player is in range and presses the pickup key, toggle the door
        if (inReach && Input.GetButtonDown("pickup"))
        {
            if (doorController != null)
            {
                doorController.ToggleDoor();
            }
            // Optional: hide the text immediately if you only show it once
            // if (interactText != null) interactText.SetActive(false);
        }
    }
}
