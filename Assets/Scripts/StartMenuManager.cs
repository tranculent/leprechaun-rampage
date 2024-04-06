using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    void Start()
    {
        // Ensure that the gameplay is paused when the MainMenu scene starts
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowUpgradeMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowOptionsMenu()
    {
        // Implement code to show the options menu
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game..");
        Application.Quit();
        FindObjectOfType<CoinsManager>().SaveCurrency();
    }
}
