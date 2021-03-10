using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoice : MonoBehaviour
{
    [SerializeField]
    private WeaponBehave[] weapons;

    private int currentWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex)
            return;

        weapons[currentWeaponIndex].gameObject.SetActive(false);

        weapons[weaponIndex].gameObject.SetActive(true);

        currentWeaponIndex = weaponIndex;
    }

    public WeaponBehave GetCurrentSelectedWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
