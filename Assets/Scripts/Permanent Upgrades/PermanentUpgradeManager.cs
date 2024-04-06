using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUpgradeManager : MonoBehaviour
{
    public UpgradeUI upgradeUI; // Reference to the UpgradeUI script

    void Start()
    {
        // Check if upgrade data exists in PlayerPrefs, if not, initialize it
        InitializeMaxHealthUpgrade();
        InitializeMaxSpeedUpgrade();
        InitializeMaxDamageUpgrade();
    }

    private void InitializeMaxHealthUpgrade()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsConsts.MaxHealthPermanentUpgrade);
        PlayerPrefs.SetInt(PlayerPrefsConsts.PlayerCurrency, 8000);
        if (!PlayerPrefs.HasKey(PlayerPrefsConsts.MaxHealthPermanentUpgrade))
        {
            PermanentUpgrade maxHealthUpgrade = new PermanentUpgrade
            {
                _upgradeKey = PlayerPrefsConsts.MaxHealthPermanentUpgrade,
                _maximumPointsToUpgrade = 10,
                _currentPoints = 0,
                _costToUpgrade = 100,
                _costIncreasePerUpgrade = 200
            };

            string maxHealthUpgradeJson = JsonUtility.ToJson(maxHealthUpgrade);
            PlayerPrefs.SetString(PlayerPrefsConsts.MaxHealthPermanentUpgrade, maxHealthUpgradeJson);
        }

        // Set UI elements for Max Health Upgrade
        upgradeUI.SetUIElements(PlayerPrefsConsts.MaxHealthPermanentUpgrade,
            GameObject.Find("MaxHealthUpgradeNameText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxHealthProgressBarImage").GetComponent<Image>(),
            GameObject.Find("MaxHealthCostText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxHealthUpgradeButton").GetComponent<Button>());
    }

    private void InitializeMaxSpeedUpgrade()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConsts.MaxSpeedPermanentUpgrade))
        {
            PermanentUpgrade maxSpeedUpgrade = new PermanentUpgrade
            {
                _upgradeKey = PlayerPrefsConsts.MaxSpeedPermanentUpgrade,
                _maximumPointsToUpgrade = 10,
                _currentPoints = 0,
                _costToUpgrade = 100,
                _costIncreasePerUpgrade = 300
            };

            string maxSpeedUpgradeJson = JsonUtility.ToJson(maxSpeedUpgrade);
            PlayerPrefs.SetString(PlayerPrefsConsts.MaxSpeedPermanentUpgrade, maxSpeedUpgradeJson);
        }

        // Set UI elements for Speed Upgrade
        upgradeUI.SetUIElements(PlayerPrefsConsts.MaxSpeedPermanentUpgrade,
            GameObject.Find("MaxSpeedUpgradeNameText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxSpeedProgressBarImage").GetComponent<Image>(),
            GameObject.Find("MaxSpeedCostText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxSpeedUpgradeButton").GetComponent<Button>());
    }

    private void InitializeMaxDamageUpgrade()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConsts.MaxDamagePermanentUpgrade))
        {
            PermanentUpgrade maxDamageUpgrade = new PermanentUpgrade
            {
                _upgradeKey = PlayerPrefsConsts.MaxDamagePermanentUpgrade,
                _maximumPointsToUpgrade = 4,
                _currentPoints = 0,
                _costToUpgrade = 100,
                _costIncreasePerUpgrade = 500
            };

            string maxDamageUpgradeJson = JsonUtility.ToJson(maxDamageUpgrade);
            PlayerPrefs.SetString(PlayerPrefsConsts.MaxDamagePermanentUpgrade, maxDamageUpgradeJson);
        }

        // Set UI elements for Speed Upgrade
        upgradeUI.SetUIElements(PlayerPrefsConsts.MaxDamagePermanentUpgrade,
            GameObject.Find("MaxDamageUpgradeNameText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxDamageProgressBarImage").GetComponent<Image>(),
            GameObject.Find("MaxDamageCostText").GetComponent<TMP_Text>(),
            GameObject.Find("MaxDamageUpgradeButton").GetComponent<Button>());
    }
}
