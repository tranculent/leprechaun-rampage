using UnityEngine;

public class Bazooka : Gun
{
    public override void Shoot()
    {
        if (HasAmmoLeft())
        {
            GetComponent<BigRookGames.Weapons.GunfireController>().FireWeapon();
        }
    }
}
