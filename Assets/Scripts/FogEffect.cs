using UnityEngine;

public class FogEffect : MonoBehaviour
{
    public Transform player;
    private ParticleSystem fog;
    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        fog = GetComponent<ParticleSystem>();
        mainModule = fog.main;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        mainModule.startColor = new Color(1, 1, 1, Mathf.Clamp01(1 - distance / 10));
    }
}