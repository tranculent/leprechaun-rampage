using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public float gravity = 9.81f;
    public float jumpForce = 5f;
    public float bhopAcceleration = 2f;
    public float bhopAccelerationCap = 4f; // Maximum allowed acceleration
    public float sprintSpeedMultiplier = 2f;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeight = 1f; // Height of the player when crouching

    private Rigidbody rb;
    private bool isGrounded;
    private float originalSpeed;
    private bool didBunnyHop;
    private bool isSprinting;
    private bool isCrouching;
    private Camera playerCamera;
    private Vector3 crouchScale;
    private Vector3 normalScale;


    private void Start()
    {
        playerCamera = GetComponent<Camera>();
        originalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted tilting

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        crouchScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        normalScale = transform.localScale;
    }

    private void Update()
    {
        // Player movement
        MovePlayer();

        // Player rotation (looking around)
        RotatePlayer();

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();

            if (!didBunnyHop)
            {
                didBunnyHop = true;
                AccelerateSpeedBhop();
            }
        }
        else if (!Input.GetButtonDown("Jump") && !isGrounded)
        {
            didBunnyHop = false;
        }

        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            Crouch(false);
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        Vector3 moveDirection = transform.TransformDirection(movement);

        // Adjust speed based on sprinting or crouching
        float currentSpeed = speed;
        if (isSprinting)
        {
            currentSpeed *= sprintSpeedMultiplier;
        }
        else if (isCrouching)
        {
            currentSpeed *= crouchSpeedMultiplier;
        }

        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate around the global Y-axis (up and down)
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Rotate around the local X-axis (upside down)
        float newRotationX = transform.eulerAngles.x - mouseY * sensitivity;
        transform.rotation = Quaternion.Euler(newRotationX, transform.eulerAngles.y, 0f);
    }

    private void Jump()
    {
        isGrounded = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void AccelerateSpeedBhop()
    {
        speed *= bhopAcceleration;
    }

    public void AccelerateSpeed(int increase)
    {
        speed *= increase;
    }

    private void Crouch(bool shouldCrouch)
    {
        if (shouldCrouch)
        {
            transform.localScale = crouchScale;
            // Lower the camera position
            playerCamera.transform.localPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        }
        else
        {
            transform.localScale = normalScale;
            // Restore the original camera position
            playerCamera.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
    }

    public void ResetSpeed()
    {
        speed = originalSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (didBunnyHop)
            {
                AccelerateSpeedBhop();
            }
            else
            {
                ResetSpeed();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Optionally, you can add code here to handle enemy collisions.
            // For now, let's apply an impulse force to counteract the collision force.
            //Vector3 collisionNormal = collision.contacts[0].normal;
            //rb.AddForce(collisionNormal * 10f, ForceMode.Impulse); // Adjust the force as needed
        }
    }

    public void IncreaseMovementSpeed(int increaseAmount)
    {
        speed += increaseAmount;
    }

    public void SetMaxSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
