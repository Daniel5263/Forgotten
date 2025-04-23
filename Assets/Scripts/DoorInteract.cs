using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public GameObject interactText;
    public DoorController doorController;

    private bool inReach = false;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

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
        if (inReach && Input.GetButtonDown("pickup"))
        {
            if (doorController != null)
            {
                doorController.ToggleDoor();
            }
        }
    }
}
