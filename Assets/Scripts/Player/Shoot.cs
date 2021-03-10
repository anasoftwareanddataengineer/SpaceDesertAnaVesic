using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private WeaponChoice weaponManager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Camera mainCam;

    private GameObject crosshair;


    private void Awake()
    {

        weaponManager = GetComponent<WeaponChoice>();

        crosshair = GameObject.FindWithTag(Tags.crosshair);

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        if(weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                BulletFired();
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    BulletFired();
                }
            }
        }
    }

    void BulletFired()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            print("We hit: " + hit.transform.gameObject.name); //debug

            if (hit.transform.tag == Tags.Enemy)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }

}
