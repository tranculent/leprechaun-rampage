using UnityEngine;

public class StandardGun : Gun
{
    public GameObject muzzleFlashPrefab; // Reference to the muzzle flash prefab
    public float muzzleFlashDuration = 0.1f; // Duration for which the muzzle flash is visible

    public float maxDistance = 100f;
    public LayerMask hitLayers; // Specify which layers the raycast should hit

    public override void Shoot()
    {
        if (HasAmmoLeft() && !isReloading)
        {
            // Display muzzle flash effect
            if (muzzleFlashPrefab != null)
            {
                // Instantiate muzzle flash prefab at the fire point position and rotation
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

                // Destroy the muzzle flash after a specified duration
                Destroy(muzzleFlash, muzzleFlashDuration);
            }

            currentAmmo--;
            UpdateAmmoTextField();

            // Cast a ray from the gun's position in the direction it's facing
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxDistance, hitLayers))
            {
                // Check if the ray hit an enemy
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Deal damage to the enemy
                    enemy.TakeDamage(damage);
                }

                // Perform any additional effects (e.g., impact particles, sound effects) here

                // Visualize the hit point (optional)
                Debug.DrawLine(firePoint.position, hit.point, Color.red, 0.1f);
            }
            else
            {
                // If the raycast doesn't hit anything, perform any necessary actions (e.g., play a miss sound effect)
            }

            // Perform any additional actions after shooting (e.g., recoil, animation)

        }
    }
}
