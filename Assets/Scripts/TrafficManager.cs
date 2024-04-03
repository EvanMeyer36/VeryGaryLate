using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq; // Added for LINQ queries

public class TrafficManager : MonoBehaviour
{
    private List<Transform> destinations = new List<Transform>(); // Possible destinations for the cars
    private List<NavMeshAgent> cars = new List<NavMeshAgent>(); // List of all cars in the scene
    private Dictionary<NavMeshAgent, float> nextChangeTimes = new Dictionary<NavMeshAgent, float>(); // Tracks time until the next destination change for each car

    public float minTimeBetweenChanges = 5f; // Minimum time between destination changes
    public float maxTimeBetweenChanges = 10f; // Maximum time between destination changes

    void Start()
    {
        // Find all NavMeshAgent components in the scene attached to objects whose names start with "Vehicle_"
        cars = FindObjectsOfType<NavMeshAgent>().Where(agent => agent.gameObject.name.StartsWith("Vehicle_")).ToList();

        // Find all GameObjects in the scene containing "Waypoint" in their name and add their Transforms to the destinations list
        destinations = GameObject.FindObjectsOfType<Transform>().Where(t => t.name.Contains("Waypoint")).ToList();

        // Initialize each car's next destination change time
        foreach (var car in cars)
        {
            nextChangeTimes[car] = Time.time + Random.Range(minTimeBetweenChanges, maxTimeBetweenChanges);
            AssignRandomDestination(car);
        }
    }

    void Update()
    {
        // Check each car to see if it's time to change its destination
        foreach (var car in cars)
        {
            if (Time.time >= nextChangeTimes[car])
            {
                AssignRandomDestination(car);
                nextChangeTimes[car] = Time.time + Random.Range(minTimeBetweenChanges, maxTimeBetweenChanges);
            }
        }
    }

    void AssignRandomDestination(NavMeshAgent car)
    {
        // Ensure there are destinations available
        if (destinations.Count == 0)
        {
            Debug.LogWarning("No destinations available.");
            return;
        }

        // Assign a random destination from the list
        Transform destination = destinations[Random.Range(0, destinations.Count)];
        car.SetDestination(destination.position);
    }
}
