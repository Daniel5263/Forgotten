using UnityEngine;

public class EnemyGrab : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float grabRange = 2f;

    private bool isGrabbing = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= grabRange && !isGrabbing)
        {
            isGrabbing = true;
            animator.SetTrigger("Grab");
        }
    }
}
