using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Player : MonoBehaviour
{
    public int health;
    public int speedPickup = 10;
    public GameObject playerHpTextField;
    public Camera mainCamera;
    public GameOverManager gameOverManager;
    public int damage = 10;

    private Gun activeGun;
    private PlayerMovement playerMovement;
    private bool hasSpeedPickupActive;
    private bool isInvincible;
    private int level = 1;

    public void Start()
    {
        GetComponent<Camera>().GetComponent<AudioListener>().enabled = true;
        hasSpeedPickupActive = false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Get active gun held by player
        activeGun = FindObjectsOfType<Gun>().Where(gun => gun.gameObject.activeSelf).ToArray()[0];
        activeGun.UpdateAmmoTextField();

        HandleGunInputs();

        if (activeGun.currentAmmo == 0)
        {
            activeGun.Reload();
        }
    }

    public void OnAmmoPickup(int ammoAmount)
    {
        if (activeGun != null)
        {
            activeGun.AddAmmo(ammoAmount);
        }
    }

    public IEnumerator OnSpeedPickupCoroutine(int speedIncrease)
    {
        hasSpeedPickupActive = true;
        playerMovement.AccelerateSpeed(speedIncrease);
        yield return new WaitForSeconds(3f);
        playerMovement.ResetSpeed();
        hasSpeedPickupActive = false;
    }

    public IEnumerator OnInvicibilityPickupCoroutine(int invicibilityTimer)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invicibilityTimer);
        isInvincible = false;
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            health -= damageAmount;
            SetPlayerHpTextField(health);

            if (health <= 0)
            {
                Die();
            }
        }
    }

    // Method to handle entity death (to be overridden by subclasses)
    private void Die()
    {
        Destroy(gameObject);

        if (gameOverManager != null)
        {
            if (mainCamera)
            {
                mainCamera.gameObject.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Show death screen
            gameOverManager.ShowGameOverScreen();
            
        }
    }

    private void SetPlayerHpTextField(int hp)
    {
        playerHpTextField.GetComponent<TextMeshProUGUI>().text = hp.ToString();
    }

    private void HandleGunInputs()
    {
        // Gun aiming - zoom in / zoom out
        if (Input.GetButtonDown("Fire2"))
        {
            activeGun.AimingInPressed();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            activeGun.AimingInRelease();
        }

        // Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            activeGun.Shoot();
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            activeGun.Reload();
        }
    }

    public bool HasSpeedPickupActive()
    {
        return hasSpeedPickupActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Speed Pickup"))
        {
            if (!HasSpeedPickupActive())
            {
                StartCoroutine(OnSpeedPickupCoroutine(speedPickup));
                Destroy(collision.gameObject);
            }
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void SetIsInvincible(bool invicibility)
    {
        isInvincible = invicibility;
    }

    public void IncreaseHp(int increaseAmount)
    {
        health += increaseAmount;
        SetPlayerHpTextField(health);
    }

    public void IncreaseDamage(int increaseAmount)
    {
        damage += increaseAmount;

        FindAnyObjectByType<WeaponManager>().UpgradeWeaponsDamage(increaseAmount);
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetMaxHealth(int healthUpgradeAmount)
    {
        Debug.Log(health);
        health += healthUpgradeAmount;
        SetPlayerHpTextField(health);
    }
}
