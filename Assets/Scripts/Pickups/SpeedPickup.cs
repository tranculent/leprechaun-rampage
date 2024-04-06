using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    public int increaseAmount = 30;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add ammo to player
            other.gameObject.GetComponent<PlayerMovement>().IncreaseMovementSpeed(increaseAmount);

            // Destroy the ammo pickup
            Destroy(gameObject);
        }
    }
}
