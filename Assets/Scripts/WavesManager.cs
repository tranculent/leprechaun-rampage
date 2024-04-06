using TMPro;
using UnityEngine;

/**
 * Waves Mechanics
 *  The game should get progressively harder with each wave
 *  For example
 *      With each wave, we spawn n more enemies
 *      Every 10 waves, enemies have more health
 *      Every 5 waves, player gets a reward (pause wave manager until reward has been picked up)
 *      
 */
// [deprecated]
public class WavesManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public GameObject waveScoreText;
    public GameObject rewardsCanvas;
    public int enemiesIncreasePerWave;
    private int currentWave;
    private bool isPaused = false;

    void Start()
    {
        currentWave = 1;
        InvokeRepeating("SpawnWave", enemySpawner.timeBetweenWaves, enemySpawner.timeBetweenWaves);
        rewardsCanvas.SetActive(false);
    }

    void SpawnWave()
    {
        if (!isPaused)
        {
            enemySpawner.enemiesToSpawn += enemiesIncreasePerWave;
            enemySpawner.SpawnEnemy();
            waveScoreText.GetComponent<TextMeshProUGUI>().text = "Wave: " + currentWave.ToString();
            currentWave++;

            if (currentWave % 5== 0)
            {
                isPaused = true;
                // Call a method to pause waves manager for rewards selection
                PauseForRewards();
            }
        }
    }

    void ResumeWaves()
    {
        isPaused = false;
        rewardsCanvas.SetActive(false); // Hide the rewards canvas when resuming waves
        Time.timeScale = 1; // Resume the game
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of the screen
        Cursor.visible = false; // Hide cursor
    }

    void PauseForRewards()
    {
        // Enable the rewards canvas to display rewards UI
        rewardsCanvas.SetActive(true);
        isPaused = true;
        Time.timeScale = 0; // Freeze the game
        Cursor.lockState = CursorLockMode.None; // Allow cursor movement
        Cursor.visible = true; // Make cursor visible
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
