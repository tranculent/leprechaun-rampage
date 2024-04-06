using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] pickups;
    public float spawnInterval = 5f;
    public float clearPickupsInterval = 10f;
    public float spawnRadius = 10f;
    public int maxPickups = 6;
    public LayerMask terrainLayerMask;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Start spawning pickups
        InvokeRepeating("SpawnPickup", 0f, spawnInterval);
        InvokeRepeating("ClearPickups", 0f, clearPickupsInterval);
    }

    void SpawnPickup()
    {
        if (GameObject.FindGameObjectsWithTag("Pickup").Length >= maxPickups || player == null)
            return;

        // Generate a random value within the total probability range
        float randomValue = Random.value;
        float cumulativeProbability = 0f;
        foreach (GameObject pickup in pickups)
        {
            Pickup pickupComponent = pickup.GetComponent<Pickup>();
            cumulativeProbability += pickupComponent._spawnProbability;

            // Check if the random value falls within the range of the current pickup's probability
            if (randomValue <= cumulativeProbability)
            {
                // Calculate random spawn position within the radius
                Vector3 spawnPosition = player.transform.position + Random.insideUnitSphere * spawnRadius;

                // Raycast downwards to find the terrain surface
                RaycastHit hit;
                if (Physics.Raycast(spawnPosition + Vector3.up * 1000f, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
                {
                    // Set spawn position to the hit point on the terrain
                    spawnPosition = hit.point;
                }

                // Spawn the selected pickup at the adjusted position
                // todo: refactor this whole method with EnemySpawner
                Instantiate(pickup, new Vector3(spawnPosition.x, 1, spawnPosition.z), Quaternion.identity);
                break;  // Exit the loop after spawning the pickup
            }
        }
    }

    void ClearPickups()
    {

    }
}