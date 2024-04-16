using UnityEngine;
using System.Collections;
using System;

public class CrowdMember : MonoBehaviour
{
    private GameManager1 gameManager;
    private AudioSource audioSource;
    private Rigidbody rb;
    public float wanderSpeed = 1.5f;
    private Vector3 wanderPoint;
    private bool isWandering = false;

    private UnityEngine.AI.NavMeshAgent agent; // Reference to the NavMesh Agent component
    public float wanderRadius = 10f; // Use this for both old wandering and NavMesh wandering
    private float nextWanderTime = 0f;
    public float wanderInterval = 5f; // Time between wanders
    public float maxForce = 10f; // The amount of force applied when hit by the player

    public Boolean bouncy;

    public Boolean explody;

    public AudioClip soundEffect;
    private AudioClip[] hitSounds; // Array of sounds that play when the NPC is hit

    void Start()
    {
        gameManager = FindObjectOfType<GameManager1>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Load all audio clips from the "Audio" subfolder in "Resources"
        hitSounds = Resources.LoadAll<AudioClip>("Audio");
    }

    void Update()
    {
        if (Time.time >= nextWanderTime && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            Wander();
            nextWanderTime = Time.time + wanderInterval;
        }
    }

    public void PlayRandomHitSound()
    {
        if (hitSounds != null && hitSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[index]);
        }
    }

    void Wander()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }

        agent.SetDestination(finalPosition);
    }

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        if (agent != null)
        {
            agent.enabled = false;
        }

        if (rb != null)
        {
            rb.isKinematic = false;
            Vector3 forceDirection = collision.transform.position - transform.position;
            forceDirection.y = 0; // Apply force horizontally
            forceDirection.Normalize();
            rb.AddForce(-forceDirection * maxForce, ForceMode.Impulse);
            isWandering = false;
        }
        if (soundEffect != null)
            audioSource.PlayOneShot(soundEffect);
        PlayRandomHitSound();
        
        // Use the singleton instance to add score
        GameManager1.Instance.AddScore(1);
    }
}

}
