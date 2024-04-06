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

    private Rigidbody rb;
    private bool isGrounded;
    private float originalSpeed;
    private bool didBunnyHop;

    private void Start()
    {
        originalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted tilting

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        Vector3 moveDirection = transform.TransformDirection(movement);
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
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

    public void ResetSpeed()
    {
        speed = originalSpeed;
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
