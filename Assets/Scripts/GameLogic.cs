using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject counter;
    public GameObject winScreen;

    public GameObject collectedPages;
    public float directionDisplayTime = 5f;

    public int pageCount { get; private set; }
    private bool hasWon = false;
    private bool hasPages = false;

    [Header("Epilogue")]
    public Text epilogueText;
    public GameObject creditsPanel;


    void Start()
    {
        pageCount = 0;
        winScreen.SetActive(false);
        if (collectedPages != null)
            collectedPages.SetActive(false);
    }

    void Update()
    {
        // update the counter
        counter.GetComponent<Text>().text = $"{pageCount}/6";

        if (!hasPages && pageCount >= 6)
        {
            hasPages = true;
            StartCoroutine(ShowDirectionPrompt());
        }
    }

    public void AddPage()
    {
        pageCount++;
    }

    public void TriggerWinSequence()
    {
        if (!hasWon)
        {
            hasWon = true;
            StartCoroutine(HandleWin());
        }
    }

    private IEnumerator ShowDirectionPrompt()
    {
        collectedPages.SetActive(true);
        yield return new WaitForSecondsRealtime(directionDisplayTime);
        collectedPages.SetActive(false);
    }

    private IEnumerator HandleWin()
    {
        Time.timeScale = 0f;

        winScreen.SetActive(true);

        yield return StartCoroutine(PlayEpilogue());

        Time.timeScale = 1f;

        SceneManager.LoadScene("CreditsScene");
    }

    private IEnumerator PlayEpilogue()
    {
        var lines = new[]
        {
        "You step out into the pale morning light… free at last.",
        "But something tugs at the edge of your memory.",
        "A single sheet of paper lies on your bedside table.",
        "“Congratulations on completing your journey,” it reads.",
        "“You were never the heroic investigator.”",
        "“You were the patient.”",
        "This entire ordeal…",
        "A new form of therapy by Dr. Karlheinz…",
        "Was designed to unlock the truth of your own confinement.",
        "Session Complete"
    };
        var delays = new[] { 3f, 2f, 3f, 3f, 2f, 2f, 2f, 3f, 3f, 2f };

        for (int i = 0; i < lines.Length; i++)
        {
            epilogueText.text = lines[i];
            yield return new WaitForSecondsRealtime(delays[i]);
        }
    }
}
