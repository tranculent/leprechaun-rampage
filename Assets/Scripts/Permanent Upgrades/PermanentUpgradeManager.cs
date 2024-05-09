using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUpgradeManager : MonoBehaviour
{
    public UpgradeUI upgradeUI; // Reference to the UpgradeUI script

    void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsConsts.PlayerCurrency, 8000);

        InitializePermanentUpgrade(PlayerPrefsConsts.MaxHealthPermanentUpgrade,
            10, 0, 100, 200, "MaxHealthUpgradeNameText", "MaxHealthUpgradeAmount", "MaxHealthProgressBarImage", "MaxHealthCostText", "MaxHealthUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.MaxSpeedPermanentUpgrade,
            10, 0, 100, 300, "MaxSpeedUpgradeNameText", "MaxSpeedUpgradeAmount", "MaxSpeedProgressBarImage", "MaxSpeedCostText", "MaxSpeedUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.MaxDamagePermanentUpgrade,
            10, 0, 100, 300, "MaxDamageUpgradeNameText", "MaxDamageUpgradeAmount", "MaxDamageProgressBarImage", "MaxDamageCostText", "MaxDamageUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.MaxReloadSpeedPermanentUpgrade,
            4, 0, 100, 500, "MaxReloadSpeedUpgradeNameText", "MaxReloadSpeedAmount", "MaxReloadSpeedProgressBarImage", "MaxReloadSpeedCostText", "MaxReloadSpeedUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.AmmoCapacityPermanentUpgrade,
            3, 0, 300, 500, "AmmoCapacityUpgradeNameText", "AmmoCapacityAmount", "AmmoCapacityProgressBarImage", "AmmoCapacityCostText", "AmmoCapacityUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.CurrencyEarningPermanentUpgrade,
            3, 0, 300, 500, "CurrencyEarningUpgradeNameText", "CurrencyEarningAmount", "CurrencyEarningProgressBarImage", "CurrencyEarningCostText", "CurrencyEarningUpgradeButton");

        /* TODO: Refine those two upgrades because they are skills and not upgrades.
         * 
         * InitializePermanentUpgrade(PlayerPrefsConsts.BulletPenetrationPermanentUpgrade,
            3, 0, 300, 500, "BulletPenetrationUpgradeNameText", "BulletPenetrationProgressBarImage", "BulletPenetrationCostText", "BulletPenetrationUpgradeButton");

        InitializePermanentUpgrade(PlayerPrefsConsts.SprintDurationPermanentUpgrade,
            3, 0, 300, 500, "SprintDurationUpgradeNameText", "SprintDurationProgressBarImage", "SprintDurationCostText", "SprintDurationUpgradeButton");
        */
    }

    private void InitializePermanentUpgrade(
        string permanentUpgradeConst, 
        int maximumPointsToUpgrade, 
        int currentPoints, 
        int costToUpgrade, 
        int costIncreasePerUpgrade,
        string buttonText,
        string amount,
        string buttonImage,
        string costText,
        string upgradeButton
        )
    {
        if (!PlayerPrefs.HasKey(permanentUpgradeConst))
        {
            PermanentUpgrade upgrade = new PermanentUpgrade
            {
                _upgradeKey = permanentUpgradeConst,
                _maximumPointsToUpgrade = maximumPointsToUpgrade,
                _currentPoints = currentPoints,
                _costToUpgrade = costToUpgrade,
                _costIncreasePerUpgrade = costIncreasePerUpgrade,
                _upgradeAmount = Int32.Parse(GameObject.Find(amount).GetComponent<TextMeshProUGUI>().text.Substring(1)) // Exclude first character as it is a '+'
            };

            string upgradeJson = JsonUtility.ToJson(upgrade);
            PlayerPrefs.SetString(permanentUpgradeConst, upgradeJson);
        }

        // Set UI elements for Speed Upgrade
        upgradeUI.SetUIElements(permanentUpgradeConst,
            GameObject.Find(buttonText).GetComponent<TMP_Text>(),
            GameObject.Find(buttonImage).GetComponent<Image>(),
            GameObject.Find(costText).GetComponent<TMP_Text>(),
            GameObject.Find(upgradeButton).GetComponent<Button>());
    }
}
