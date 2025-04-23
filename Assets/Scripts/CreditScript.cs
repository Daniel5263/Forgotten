using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    public float scrollSpeed = 115f;
    public float endPosition = 1483f;

    private RectTransform rectTransform;
    private bool hasLoadedMenu = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (hasLoadedMenu) return;

        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectTransform.anchoredPosition.y >= endPosition)
        {
            hasLoadedMenu = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
