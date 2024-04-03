using UnityEngine;
using UnityEngine.AI;

public class WanderScript : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public float knockOverForce; // Add this line

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    // Initialize
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer; // Initialize the timer with your wander time
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Use appropriate tag or condition
        {
            Vector3 forceDirection = transform.position - collision.transform.position;
            forceDirection.y = 0; // Keep the force horizontal
            forceDirection.Normalize();
            GetComponent<Rigidbody>().AddForce(forceDirection * knockOverForce, ForceMode.Impulse);
        }
    }
}
