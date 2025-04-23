using System.Collections;
using UnityEngine;

public class FinalDoorInteract : MonoBehaviour
{
    [Header("Door & UI")]
    public DoorController doorController;
    public GameObject interactText;
    public GameObject needMorePagesText;

    [Header("Final Door")]
    public bool isFinalDoor = false;
    public float postOpenDelay = 1f;

    private bool inReach = false;
    private GameLogic gameLogic;

    void Start()
    {
        gameLogic = GameObject.FindWithTag("GameLogic").GetComponent<GameLogic>();
        if (interactText) interactText.SetActive(false);
        if (needMorePagesText) needMorePagesText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (interactText) interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (interactText) interactText.SetActive(false);
            if (needMorePagesText) needMorePagesText.SetActive(false);
        }
    }

    void Update()
    {
        if (!inReach || !Input.GetButtonDown("pickup"))
            return;

        if (!isFinalDoor)
        {
            doorController.ToggleDoor();
            return;
        }

        if (gameLogic.pageCount >= 6)
        {
            doorController.ToggleDoor();
            StartCoroutine(DelayedWin());
        }
        else if (needMorePagesText)
        {
            StartCoroutine(FlashNeedPages());
        }
    }

    private IEnumerator DelayedWin()
    {
        yield return new WaitForSecondsRealtime(postOpenDelay);
        gameLogic.TriggerWinSequence();
    }

    private IEnumerator FlashNeedPages()
    {
        needMorePagesText.SetActive(true);
        yield return new WaitForSeconds(2f);
        needMorePagesText.SetActive(false);
    }
}
