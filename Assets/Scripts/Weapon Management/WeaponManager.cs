using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Mouse wheel input for weapon switching
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            ChangeWeapon((mouseWheel > 0) ? 1 : 0);
        }

        // Number key input for weapon switching
        for (int i = 1; i <= weapons.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                ChangeWeapon(i - 1);
            }
        }
    }

    void ChangeWeapon(int index)
    {
        currentWeaponIndex = (index + weapons.Length) % weapons.Length;
        SwitchWeapon(currentWeaponIndex);
    }

    void SwitchWeapon(int index)
    {
        Debug.Log(weapons.Length);
        // Disable all weapons
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // Enable the selected weapon
        weapons[index].SetActive(true);
    }

    public void UpgradeWeapons(int damageAmount)
    {
        if (weapons != null)
        {
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<Gun>().IncreaseDamage(damageAmount);
            }
        }
    }
}
