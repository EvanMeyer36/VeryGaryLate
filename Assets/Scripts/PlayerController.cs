using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject camHolder;
    public float speed, sensitivity, maxForce, jumpForce;
    private Vector2 move, look;
    private float lookRotation;
    public bool grounded;
    
    public GameObject directionArrow;
    public Transform checkpoint;

    // Reference to the GameManager script
    public GameManager1 gameManager;

    private int bounceForce = 2000;

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("crowd")) // Assuming 'crowd' is the tag of your NPCs
    {
        Rigidbody npcRigidbody = collision.collider.GetComponent<Rigidbody>();
        if (npcRigidbody != null)
        {
            npcRigidbody.isKinematic = false; // NPCs can now be affected by physics
            Vector3 forceDirection = collision.transform.position - transform.position;
            forceDirection.y = 0; // Ensure the force is applied horizontally
            forceDirection.Normalize();
            npcRigidbody.AddForce(forceDirection * maxForce, ForceMode.Impulse);
            if (collision.gameObject.GetComponent<CrowdMember>().bouncy)
                rb.AddExplosionForce(bounceForce, collision.contacts[0].point,1);
                // Debug.Log("Bouncy");
        
        }
    }
}

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(!gameManager.isPause){
            look = context.ReadValue<Vector2>();
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y) * speed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        // Limit force
        rb.AddForce(Vector3.ClampMagnitude(velocityChange, maxForce), ForceMode.VelocityChange);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * look.x * sensitivity);

        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void LateUpdate()
    {
        Look();
        UpdateDirectionArrow();
    }

    public void SetGrounded(bool state)
    {
        grounded = state;
    }

void UpdateDirectionArrow()
{
    if (directionArrow != null && checkpoint != null)
    {
        // Get the flat direction from the player to the checkpoint
        Vector3 direction = (checkpoint.position - transform.position).normalized;
        direction.y = 0; // We flatten the direction to keep it on the horizontal plane

        // Since the arrow's 'forward' is along Unity's Y-axis, we create a new direction vector for the arrow
        Vector3 arrowDirection = new Vector3(direction.x, direction.z, 0).normalized; // Swap Y and Z

        // Now, we need to find the rotation that points the arrow's 'up' (which is its 'forward' in model terms) towards the target
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, arrowDirection); // Vector3.forward is a placeholder for the arrow's 'forward'

        // Apply the rotation to the arrow
        directionArrow.transform.rotation = lookRotation;
    }
}



}
