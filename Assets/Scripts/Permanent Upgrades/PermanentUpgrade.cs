using UnityEngine;

public class PermanentUpgrade
{
    public string _upgradeKey;
    public int _maximumPointsToUpgrade;
    public int _currentPoints;
    public int _costToUpgrade;
    public int _costIncreasePerUpgrade;

    private int availableCoins = PlayerPrefs.GetInt(PlayerPrefsConsts.PlayerCurrency);

    public void _OnClick()
    {
        // If the current points of this upgrade hasn't reached its maximum points to upgrade and the player has enough coins to purchase it, buy it and save it in the player prefs
        if (_currentPoints < _maximumPointsToUpgrade && availableCoins > _costToUpgrade)
        {
            // Store updated information for available coins for the player
            PlayerPrefs.SetInt(PlayerPrefsConsts.PlayerCurrency, availableCoins - _costToUpgrade);

            _currentPoints++;
            _costToUpgrade += _costIncreasePerUpgrade;

            // Store updated information for the current upgrade
            PlayerPrefs.SetString(_upgradeKey, JsonUtility.ToJson(this));

            // Save prefs
            PlayerPrefs.Save();
        } 
        else
        {
            Debug.Log("Maximum points already reached for this upgrade or not enough coins to purchase!");
        }
    }
}
