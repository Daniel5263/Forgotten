using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Player & NavMesh")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Teleport Settings")]
    public float teleportDistance = 12f;
    public float minTeleportCooldown = 2f;
    public float maxTeleportCooldown = 6f;
    [Range(0f, 1f)] public float chaseProbability = 0.8f;
    private float teleportTimer;

    [Header("Chase Settings")]
    public float chaseRange = 8f;

    [Header("Static Visual Only")]
    public GameObject staticObject;
    public float staticActivationRange = 20f;

    [Header("Teleport Audio")]
    public AudioClip teleportSound;
    private AudioSource audioSource;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        teleportTimer = Random.Range(minTeleportCooldown, maxTeleportCooldown);

        agent = GetComponent<NavMeshAgent>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (staticObject) staticObject.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // visual static flicker
        if (staticObject)
            staticObject.SetActive(dist <= staticActivationRange);

        // if close enough, chase via NavMeshAgent
        if (dist <= chaseRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            return;
        }

        // otherwise handle teleport
        agent.isStopped = true;
        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f)
        {
            bool teleportNear = Random.value <= chaseProbability;
            Vector3 target = teleportNear
                ? player.position + Random.onUnitSphere * teleportDistance
                : basePosition;

            agent.Warp(target);
            audioSource.PlayOneShot(teleportSound);

            teleportTimer = Random.Range(minTeleportCooldown, maxTeleportCooldown);
        }
    }
}
