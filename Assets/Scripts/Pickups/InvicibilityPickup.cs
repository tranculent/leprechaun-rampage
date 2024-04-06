using UnityEngine;

public class InvicibilityPickup : Pickup
{
    public int invicibilityTimer;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add ammo to player
            other.gameObject.GetComponent<Player>().OnInvicibilityPickupCoroutine(invicibilityTimer);

            // Destroy the ammo pickup
            Destroy(gameObject);
        }
    }
}
