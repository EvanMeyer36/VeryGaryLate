using UnityEngine;

public class ArrowPointing : MonoBehaviour
{
    public Transform target; // The target object the arrow should point to
    public Transform player; // The player to which the arrow is attached
    public float yOffset = 2.0f; // Height offset from the player's position
    public float zOffset = 2.0f; // Forward offset from the player's position
    public float rotationSpeed = 5.0f; // How fast the arrow rotates towards the target

    void Start()
    {
        SetInitialPosition(); // Set the initial position of the arrow relative to the player
    }

    void LateUpdate()
    {
        if (target != null && player != null)
        {
            SetInitialPosition(); // Update the arrow's position to follow the player

            // Calculate the direction from the arrow to the target on the horizontal plane
            Vector3 directionToTarget = new Vector3(target.position.x - transform.position.x, 90, target.position.z - transform.position.z);

            if (directionToTarget.sqrMagnitude > 0.0001) // Check to avoid division by zero
            {
                // Determine the correct rotation for the arrow to point towards the target
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                Quaternion correctedRotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);

                // Smoothly rotate the arrow towards the target
                transform.rotation = Quaternion.Slerp(transform.rotation, correctedRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void SetInitialPosition()
    {
        // Ensure the arrow starts above and in front of the player but within view
        transform.position = player.position + Vector3.up * yOffset + player.forward * zOffset;
    }
}
