using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class CarController : MonoBehaviour
{
    public Transform[] waypoints; // Array of all waypoints
    public float speed = 10f; // Public variable to control speed
    private int currentWaypointIndex = 0; // Current waypoint index
    private NavMeshAgent carAgent;
    private bool isTravelling; // Added to keep track of whether the car is currently travelling to a waypoint

    void Start() {
    carAgent = GetComponent<NavMeshAgent>();
    carAgent.speed = speed;
    carAgent.avoidancePriority = Random.Range(1, 100); // Ensure diverse priorities among cars

        if (waypoints.Length == 0)
        {
            // Debug.LogError("No waypoints set!");
        }
        else
        {
            MoveToNextWaypoint();
            isTravelling = true; // Start travelling to the first waypoint
        }
    }

    void Update()
    {
        // Check if car has reached its current destination
        if (isTravelling && !carAgent.pathPending && carAgent.remainingDistance <= carAgent.stoppingDistance + 0.5f) // Added a small buffer for accuracy
        {
            isTravelling = false; // Stop travelling
            // Move to the next waypoint after a short delay to ensure it doesn't skip over waypoints
            Invoke("MoveToNextWaypoint", 0.5f); // Adjust the delay as needed
        }
    }

    void MoveToNextWaypoint()
{
    if (waypoints.Length == 0)
        return;

    // Ensure that we reset travelling state and move to the next waypoint
    isTravelling = true;

    Debug.Log("Moving to waypoint: " + currentWaypointIndex); // Log the current waypoint index

    // Set the car to move to the current waypoint
    carAgent.SetDestination(waypoints[currentWaypointIndex].position);

    // Update the waypoint index, loop back to 0 if at the end of the array
    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    
    // If the next waypoint is the first one, we have completed a loop
    if (currentWaypointIndex == 0)
    {
        Debug.Log("Completed a loop!");
        // Additional logic can be added here if needed
    }
}

}
