using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float normalFOV = 60f;
    public float aimFOV = 30f;
    public float reloadTime = 2f;
    public int bulletSpeed = 100;
    public int currentAmmo = 30;
    public int maxAmmo = 120;
    public int magazine = 30;
    public TextMeshProUGUI ammoText;
    public int damage = 2;
    public bool isAimingIn;
    public Vector3 weaponAnimationVelocity = new Vector3(0.5f, 0.5f, 0.5f);

    public Transform sightTarget;
    public float sightOffSet; // how far the weapon is away from the camera
    public float aimingInTime;

    private Camera playerCamera;  // Reference to the player's cameraprivate bool isReloading = false;
    private bool isReloading;
    private int maxAmmoSize;

    // public GameObject impactEffect; // Particle system or other impact effect
    // public AudioClip impactSound;   // Sound to play on impact

    void Start()
    {
        // Try to find the camera component in the parent GameObject
        playerCamera = FindAnyObjectByType<Player>().GetComponent<Camera>();
        isReloading = false;
        maxAmmoSize = maxAmmo;
        UpdateAmmoTextField();

        if (playerCamera == null)
        {
            // If not found, log a warning
            Debug.LogWarning("Player camera not found in the parent GameObject.");
        }
    }

    private void Update()
    {
        CalculateAimingIn();
    }

    public void Shoot()
    {
        if (HasAmmoLeft())
        {
            if (!isReloading)
            {
                var bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet>().SetGun(this); // Set the bullet to belong to this gun and derive its damage
                // bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
                bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed);
                // Subtract ammo
                currentAmmo--;

                // Update UI
                UpdateAmmoTextField();
            }
            else
            {
                // Play empty ammo sound or provide feedback
            }
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        // Add remaining ammo to the maxAmmo as long as it won't go over the maxAmmo limit
        if (ammoAmount + currentAmmo > magazine)
        {
            maxAmmo += Mathf.Min(maxAmmo + ((ammoAmount + currentAmmo) - magazine), maxAmmoSize);
        }

        // Use Mathf.min to ensure that the new ammo amount won't exceed the maximum allowed amount
        currentAmmo = Mathf.Min(currentAmmo + ammoAmount, magazine);

        UpdateAmmoTextField();
    }

    private void CalculateAimingIn()
    {
        var targetPosition = transform.parent.position;

        if (isAimingIn)
        {
            targetPosition = playerCamera.transform.position + (transform.parent.position - sightTarget.position) + (playerCamera.transform.forward * sightOffSet);
            // playerCamera.fieldOfView = aimFOV;
        } 
        else
        {
            playerCamera.fieldOfView = normalFOV;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref weaponAnimationVelocity, aimingInTime);
    }

    public void AimingInPressed()
    {
        Debug.Log(true);
        isAimingIn = true;
    }

    public void AimingInRelease()
    {
        isAimingIn = false;
        Debug.Log(false);
    }

    public void SetAimState(bool state)
    {
        // Adjust FOV and other properties based on the aim state
        playerCamera.fieldOfView = state ? aimFOV : normalFOV;
        // transform.position = new Vector3(player.transform.position.x, -0.2f, player.transform.position.z + 0.34f);
    }

    public void ResetGunView()
    {
       
    }

    public void UpdateAmmoTextField()
    {
        ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < magazine && HasAmmoLeft())
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if (HasAmmoLeft())
        {
            isReloading = true;
            // Play reload animation or sound if needed

            yield return new WaitForSeconds(reloadTime);

            maxAmmo -= (magazine - currentAmmo);
            currentAmmo = magazine;
            isReloading = false;
        }
    }

    public bool HasAmmoLeft()
    {
        return currentAmmo > 0 || maxAmmo > 0;
    }

    public void IncreaseDamage(int increaseAmount)
    {
        damage += increaseAmount;
    }

    /*
    private void ShowImpactEffect()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
    }

    private void PlayImpactSound()
    {
        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }
    }
    */
}