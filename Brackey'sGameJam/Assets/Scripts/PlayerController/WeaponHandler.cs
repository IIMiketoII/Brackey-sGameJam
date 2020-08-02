using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If the player holds down the mouse will the weapon fire once (single) or continous (multiple)?
public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponSpread
{
    SPREAD,
    NONE
}

public enum WeaponReloadSpeed
{
    FAST,
    SLOW,
    NONE
}

public enum WeaponChargeSpeed
{
    SLOW,
    NONE
}

public enum WeaponAOE
{
    AOE,
    NONE
}

public enum WeaponAmmoType
{
    UNLIMITED,
    LIMITED,
    ONE,
    NONE
}

public enum WeaponBulletType
{
    LASER,
    BULLET,
    ARROW,
    ROCK
}

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public WeaponSpread spreadType;
    public WeaponReloadSpeed reloadSpeedType;
    public WeaponChargeSpeed chargeSpeedType;
    public WeaponAOE AOEType;
    public WeaponAmmoType ammoType;

    private void Awake()
    {
        
    }

    // Shoot Animation?

    /*void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }*/

    /*void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }*/


}
