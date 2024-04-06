using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammoAmount = 30;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add ammo to player
            other.gameObject.GetComponent<Player>().OnAmmoPickup(ammoAmount);

            // Destroy the ammo pickup
            Destroy(gameObject);
        }
    }
}