using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameFlowManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public Camera mainCamera;
    public GameObject rewardsCanvas;
    public Image expBar;
    public Player player;
    public float currentExperience = 1;
    public float maxExperience = 100;
    public int level = 1;

    public GameObject upgradeAvailableText;
    public GameObject upgradeAvailableIcon;

    private bool isPaused = false;
    private float currentExperienceTemp;
    private int enemiesKilled = 0;

    void Start()
    {
        currentExperienceTemp = currentExperience;
        rewardsCanvas.SetActive(false);
        upgradeAvailableIcon.SetActive(false);
        upgradeAvailableText.SetActive(false);

        player = FindAnyObjectByType<Player>();
        InvokeRepeating("SpawnWave", enemySpawner.timeBetweenWaves, enemySpawner.timeBetweenWaves);
        UpdateUI();
    }

    private void Update()
    {
        // Pause for rewards whenever we level up
        if (currentExperience >= maxExperience)
        {
            upgradeAvailableIcon.SetActive(true);
            upgradeAvailableText.SetActive(true);

            // Save exceeding xp points to the next level
            currentExperienceTemp = currentExperience - maxExperience;
            expBar.fillAmount = currentExperienceTemp;

            if (Input.GetKeyDown(KeyCode.F))
            {
                PauseForRewards();
                LevelUp();
            }
        }

        // Add currency when enough enemies have been eliminated
        if (enemiesKilled == 10)
        {
            FindObjectOfType<CoinsManager>().AddCurrency(10);
            enemiesKilled = 0;
        }
    }

    private void SpawnWave()
    {
        if (!isPaused)
        {
            enemySpawner.enemiesToSpawn = 5;
            enemySpawner.SpawnEnemy();
        }
    }

    public void IncreaseXp()
    {
        currentExperience += 40;
        currentExperienceTemp += 40;

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        maxExperience += 100;
    }

    public int GetLevel()
    {
        return level;
    }

    void UpdateUI()
    {
        expBar.fillAmount = currentExperience / maxExperience;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        maxExperience = 100;
        currentExperience = 0;
        currentExperienceTemp = 0;
        level = 1;
        Time.timeScale = 1;

        if (mainCamera)
        {
            mainCamera.gameObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        player.gameObject.GetComponent<PostProcessVolume>().enabled = false;
        isPaused = false;
        rewardsCanvas.SetActive(false); // Hide the rewards canvas when resuming waves
        upgradeAvailableIcon.SetActive(false);
        upgradeAvailableText.SetActive(false);
        Time.timeScale = 1; // Resume the game
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of the screen
        Cursor.visible = false; // Hide cursor
        player.GetComponent<CustomCameraController>().ToggleCamera(true);
    }

    void PauseForRewards()
    {
        player.GetComponent<CustomCameraController>().ToggleCamera(false);
        player.gameObject.GetComponent<PostProcessVolume>().enabled = true; // add blurry background
        // Enable the rewards canvas to display rewards UI
        rewardsCanvas.SetActive(true);
        Time.timeScale = 0; // Freeze the game
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // Allow cursor movement
        Cursor.visible = true; // Make cursor visible
        currentExperience -= maxExperience;
    }

    public void ResetExpBar()
    {
        expBar.fillAmount = 0;
    }

    public void IncreaseEnemyKilled()
    {
        enemiesKilled++;
    }
}
