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
        InitializeUpgrade(PlayerPrefsConsts.MaxDamagePermanentUpgrade);
        InitializeUpgrade(PlayerPrefsConsts.MaxReloadSpeedPermanentUpgrade);
    }

    private void InitializeUpgrade(string upgradeKey)
    {
        PermanentUpgrade upgrade = LoadUpgradeData(upgradeKey);
        int upgradeIncreaseMaxValue = upgrade._upgradeAmount * upgrade._currentPoints;
        Player playerComponent = player.GetComponent<Player>();
        PlayerMovement playerMovementComponent = player.GetComponent<PlayerMovement>();
        WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();

        if (upgrade != null)
        {
            // Apply upgrade data to player based on upgrade key
            switch (upgradeKey)
            {
                case PlayerPrefsConsts.MaxHealthPermanentUpgrade:
                    // Apply max health upgrade data to player
                    playerComponent.SetMaxHealth(upgradeIncreaseMaxValue);
                    break;
                case PlayerPrefsConsts.MaxSpeedPermanentUpgrade:
                    // Apply speed upgrade data to player
                    playerMovementComponent.SetMaxSpeed(playerMovementComponent.speed + upgradeIncreaseMaxValue);
                    break;
                case PlayerPrefsConsts.MaxDamagePermanentUpgrade:
                    // Upgrade weapons damage
                    weaponManager.UpgradeWeaponsDamage(upgradeIncreaseMaxValue);
                    break;
                case PlayerPrefsConsts.MaxReloadSpeedPermanentUpgrade:
                    // Upgrade weapons damage
                    weaponManager.UpgradeWeaponsReloadSpeed(upgradeIncreaseMaxValue);
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
