using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // horizontal movement speed
    public float jumpForce = 6f;    // upward force for jumping
    public float sprintSpeed = 22f;  // added sprint speed

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

        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                      ? sprintSpeed : moveSpeed;
        //if sprint key is held


        // Get the camera’s forward and right directions
        Transform cam = Camera.main.transform;
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Flatten vectors (remove vertical tilt)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Convert input to camera-relative movement
        Vector3 movement = (camForward * v + camRight * h).normalized * currentSpeed;

        // Apply movement while preserving vertical velocity
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
