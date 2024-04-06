using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healthAmount = 30;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add ammo to player
            other.gameObject.GetComponent<Player>().IncreaseHp(healthAmount);

            // Destroy the ammo pickup
            Destroy(gameObject);
        }
    }
}