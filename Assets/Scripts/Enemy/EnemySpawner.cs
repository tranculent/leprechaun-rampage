using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meleeEnemyPrefabs;
    public GameObject rangedEnemyPrefabs;
    public int enemiesToSpawn = 1;
    public float spawnRadius = 10f;
    public int maxEnemies = 1000;
    public LayerMask terrainLayerMask;
    public float spawnDelay = 1f;
    public GameObject enemiesCounterText;
    public int timeBetweenWaves = 5;
    public int minDistanceBetweenEnemies = 10;

    private GameObject player;
    private int currentSpawns = 0;

    private Terrain terrain;
    private Vector3 terrainSize;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            terrainSize = terrain.terrainData.size;
            SpawnEnemy();
        }
        else
        {
            Debug.LogError("No active terrain found.");
        }
    }

    private void Update()
    {
        enemiesCounterText.GetComponent<TextMeshProUGUI>().text = "Enemies spawned: " + currentSpawns.ToString();
    }

    public void SpawnEnemy()
    {
        if (currentSpawns >= maxEnemies || player == null || terrain == null)
            return;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            List<GameObject> enemyPrefabs = new List<GameObject>();
            float randomValue = Random.value;

            if (randomValue < 0.99f)
            {
                enemyPrefabs.Add(meleeEnemyPrefabs);
            }
            else
            {
                enemyPrefabs.Add(rangedEnemyPrefabs);
            }

            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, 0f, randomPoint.y);
            spawnPosition += player.transform.position;

            // Constrain spawn position within terrain boundaries
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, terrain.transform.position.x, terrain.transform.position.x + terrainSize.x);
            spawnPosition.z = Mathf.Clamp(spawnPosition.z, terrain.transform.position.z, terrain.transform.position.z + terrainSize.z);

            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 1000f, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
            {
                spawnPosition = hit.point;
            }

            foreach (GameObject enemyPrefab in enemyPrefabs)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnPosition += Random.insideUnitSphere * minDistanceBetweenEnemies;
            }
        }

        currentSpawns += enemiesToSpawn;
    }
}
