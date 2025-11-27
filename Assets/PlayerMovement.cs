using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // horizontal movement speed
    public float jumpForce = 6f;    // upward force for jumping

    private Rigidbody rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input: WASD or arrow keys
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Create movement vector in local space (XZ only)
        Vector3 movement = new Vector3(h, 0, v).normalized * moveSpeed;

        // Move player (preserve vertical velocity for jumping/gravity)
        Vector3 newVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        rb.linearVelocity = newVelocity;
    }

    void Update()
    {
        // Jump when space is pressed and grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Set isGrounded TRUE when touching a ground tagged object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Set isGrounded FALSE when leaving ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
