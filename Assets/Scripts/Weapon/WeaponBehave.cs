using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET
}
public class WeaponBehave : MonoBehaviour
{
    //private Animator aim;

    //public WeaponAim weaponAim;

    [SerializeField]
    private AudioSource shootSound, reloadSound;

    public GameObject muzzleFlash;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attackPoint;
    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }

    void PlayReloadSound()
    {
        reloadSound.Play();
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);

    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }


}
