using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the AI to follow
    public float knockOverForce = 10f; // Add this line: the force applied to crowd members
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        // Check if the AI has reached the current waypoint
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[currentWaypointIndex].position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("crowd"))
        {
            // Make sure your crowd members have a Rigidbody to apply force to
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDirection = other.transform.position - transform.position;
                forceDirection.y = 0; // Keep the force horizontal
                forceDirection.Normalize();
                rb.AddForce(forceDirection * knockOverForce, ForceMode.Impulse);
            }
        }
    }
}
