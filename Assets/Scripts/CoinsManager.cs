using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject coinsTextField;
    
    private const string currencyKey = PlayerPrefsConsts.PlayerCurrency;
    private int playerCurrency = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Load player currency from PlayerPrefs
        playerCurrency = PlayerPrefs.GetInt(currencyKey, 0);

        if (coinsTextField != null) // Only valid for start menu
            coinsTextField.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("PlayerCurrency").ToString();
    }

    // Add currency to the player's total
    public void AddCurrency(int amount)
    {
        playerCurrency += amount;
        SaveCurrency();
    }

    // Subtract currency from the player's total
    public void SubtractCurrency(int amount)
    {
        playerCurrency -= amount;
        SaveCurrency();
    }

    // Get the player's current currency amount
    public int GetCurrency()
    {
        return playerCurrency;
    }

    // Save the player's currency to PlayerPrefs
    public void SaveCurrency()
    {
        PlayerPrefs.SetInt(currencyKey, playerCurrency);
        PlayerPrefs.Save();
    }
}
