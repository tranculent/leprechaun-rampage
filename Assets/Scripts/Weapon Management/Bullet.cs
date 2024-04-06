using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 4f;
    public int headshotDamage;
    private TrailRenderer trailRenderer;
    private Gun attachedGun; // Set during runtime

    private void Start()
    {
        // Destroy the bullet after a specified lifetime
        Destroy(gameObject, lifetime);

        // Initialize and configure the Trail Renderer
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true; // Enable the trail
            trailRenderer.time = lifetime; // Set trail duration to match bullet's lifetime
        }
    }

    private void Update()
    {
        // Cast a ray in the direction of bullet's movement every frame
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime))
        {
            // Check if the ray hit an enemy
            var enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (hit.collider.transform.tag == "Headshot")
                {
                    enemy.Die();
                }
                else if (hit.collider.transform.tag == "Enemy")
                {
                    if (attachedGun != null)
                        enemy.TakeDamage((int)(FindAnyObjectByType<Player>().damage + attachedGun.damage));
                }

                // Disable the Trail Renderer when the bullet hits any object
                if (trailRenderer != null)
                {
                    trailRenderer.emitting = false;
                }

                // Destroy the bullet
                Destroy(gameObject);
            }
        }
    }

    public void SetGun(Gun gun)
    {
        attachedGun = gun;
    }
}