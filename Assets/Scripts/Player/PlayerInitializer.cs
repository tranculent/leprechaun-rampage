using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Load and apply upgrade data from PlayerPrefs
        InitializePlayerUpgrades();
    }

    private void InitializePlayerUpgrades()
    {
        InitializeUpgrade(PlayerPrefsConsts.MaxHealthPermanentUpgrade);
        InitializeUpgrade(PlayerPrefsConsts.MaxSpeedPermanentUpgrade);
        // Add more upgrades as needed...
    }

    private void InitializeUpgrade(string upgradeKey)
    {
        PermanentUpgrade upgrade = LoadUpgradeData(upgradeKey);
        int upgradeIncreaseMaxValue = (upgrade._currentPoints + 1) * 10; // +1 because it starts from zero
        Player playerComponent = player.GetComponent<Player>();
        PlayerMovement playerMovementComponent = player.GetComponent<PlayerMovement>();
        if (upgrade != null)
        {
            // Apply upgrade data to player based on upgrade key
            switch (upgradeKey)
            {
                case PlayerPrefsConsts.MaxHealthPermanentUpgrade:
                    Debug.Log("Player health: " + playerComponent.health);
                    Debug.Log("Upgrade increase max health value: " + upgradeIncreaseMaxValue);
                    // Apply max health upgrade data to player
                    playerComponent.SetMaxHealth(playerComponent.health + upgradeIncreaseMaxValue); 
                    break;
                case PlayerPrefsConsts.MaxSpeedPermanentUpgrade:
                    // Apply speed upgrade data to player
                    playerMovementComponent.SetMaxSpeed(playerMovementComponent.speed + upgradeIncreaseMaxValue);
                    break;
                // Add cases for other upgrades as needed...
                default:
                    Debug.LogWarning("Unhandled upgrade key: " + upgradeKey);
                    break;
            }
        }
    }

    private PermanentUpgrade LoadUpgradeData(string upgradeKey)
    {
        string upgradeJson = PlayerPrefs.GetString(upgradeKey);
        PermanentUpgrade upgrade = JsonUtility.FromJson<PermanentUpgrade>(upgradeJson);
        return upgrade;
        
    }
}
