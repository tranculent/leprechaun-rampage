using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 originalPosition;
    void Start()
    {
        // Store the original position of the player
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a melee enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reset the player's position to the original position
            transform.position = originalPosition;
        }
    }
}
