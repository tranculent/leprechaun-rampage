using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject deathScreenScoreField;

    private GameFlowManager gameFlowManager;

    void Start()
    {
        gameFlowManager = GameObject.FindGameObjectWithTag("GameFlowManager").GetComponent<GameFlowManager>();
        // Ensure the game over screen is initially inactive
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0; // Freeze the game

        // Activate the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            deathScreenScoreField.GetComponent<TextMeshProUGUI>().text = "You reached level: " + gameFlowManager.GetLevel().ToString(); 
        }
    }
}
