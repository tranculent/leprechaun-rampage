using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UpgradeUI : MonoBehaviour
{
    private Dictionary<string, UpgradeUIElements> upgradeUIElements = new Dictionary<string, UpgradeUIElements>();
    public GameObject availableCoinsText;

    private void Start()
    {
        availableCoinsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(PlayerPrefsConsts.PlayerCurrency).ToString();
    }

    [System.Serializable]
    public class UpgradeUIElements
    {
        public TMP_Text upgradeNameText;
        public Image pointsProgressBarImage;
        public TMP_Text costText;
        public Button upgradeButton;
    }

    // Method to set UI elements for each upgrade
    public void SetUIElements(string upgradeKey,
        TMP_Text upgradeName, 
        Image progressBar,
        TMP_Text cost, 
        Button button)
    {
        UpgradeUIElements uiElements = new UpgradeUIElements
        {
            upgradeNameText = upgradeName,
            pointsProgressBarImage = progressBar,
            costText = cost,
            upgradeButton = button
        };
        upgradeUIElements[upgradeKey] = uiElements;
        InitializeUI(upgradeKey);
    }

    private void InitializeUI(string upgradeKey)
    {
        UpgradeUIElements uiElements = upgradeUIElements[upgradeKey];
        string loadedUpgradeJson = PlayerPrefs.GetString(upgradeKey);
        PermanentUpgrade loadedUpgrade = JsonUtility.FromJson<PermanentUpgrade>(loadedUpgradeJson);
        // Check if permanent upgrade exists
        if (loadedUpgrade != null)
        {
            UpdateUI(loadedUpgrade, uiElements);
        }
    }

    public void OnUpgradeButtonClick(string upgradeKey)
    {
        Debug.Log(upgradeKey);
        if (upgradeUIElements.ContainsKey(upgradeKey))
        {
            UpgradeUIElements uiElements = upgradeUIElements[upgradeKey];
            // Load previously stored information for current permanent upgrade
            string loadedUpgradeJson = PlayerPrefs.GetString(upgradeKey);

            Debug.Log(loadedUpgradeJson);
            PermanentUpgrade loadedUpgrade = JsonUtility.FromJson<PermanentUpgrade>(loadedUpgradeJson);

            // Check if permanent upgrade exists
            if (loadedUpgrade != null)
            {
                // Update upgrade configuration
                loadedUpgrade._OnClick();

                PermanentUpgrade updatedUpgrade = JsonUtility.FromJson<PermanentUpgrade>(PlayerPrefs.GetString(upgradeKey));
                // Update UI after the upgrade
                UpdateUI(updatedUpgrade, uiElements);
            }
        }
        else
        {
            Debug.LogWarning("UpgradeUI: UI elements not set for upgrade key: " + upgradeKey);
        }
    }

    // Update UI elements with properties of the selected upgrade
    private void UpdateUI(PermanentUpgrade upgrade, UpgradeUIElements uiElements)
    {
        // Update progress bar fill amount based on current points and maximum points
        float fillAmount = (float)upgrade._currentPoints / upgrade._maximumPointsToUpgrade;
        uiElements.pointsProgressBarImage.fillAmount = fillAmount;
        uiElements.costText.text = upgrade._costToUpgrade.ToString();
        availableCoinsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(PlayerPrefsConsts.PlayerCurrency).ToString();

        // Disable upgrade button if max points reached or not enough coins
        uiElements.upgradeButton.interactable = upgrade._currentPoints < upgrade._maximumPointsToUpgrade &&
            PlayerPrefs.GetInt(PlayerPrefsConsts.PlayerCurrency) >= upgrade._costToUpgrade;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
