using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyGrab : MonoBehaviour
{
    [Header("Grab Settings")]
    public Animator animator;
    public Transform player;
    public float grabRange = 2f;
    public float grabCooldown = 2f;
    private float lastGrabTime = -Mathf.Infinity;

    [Header("Jumpscare")]
    public GameObject jumpscarePanel;
    public float jumpscareDuration = 2f;
    public string mainMenuSceneName = "MainMenu";

    private bool isJumpscaring = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (jumpscarePanel != null)
            jumpscarePanel.SetActive(false);
    }

    void Update()
    {
        if (isJumpscaring || player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= grabRange && Time.time - lastGrabTime >= grabCooldown)
        {
            lastGrabTime = Time.time;
            animator.SetTrigger("Grab");
            StartCoroutine(HandleJumpscare());
        }
    }

    private IEnumerator HandleJumpscare()
    {
        isJumpscaring = true;

        if (jumpscarePanel != null)
            jumpscarePanel.SetActive(true);

        yield return new WaitForSecondsRealtime(jumpscareDuration);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
