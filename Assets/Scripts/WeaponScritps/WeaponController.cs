using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Transform weaponHoldPosition;
    public Weapon startingWeapon;
    Weapon equipedWeapon;

    private void Start()
    {
        if (startingWeapon != null)
        {
            EquipWeapon(startingWeapon);
        }
    }
    public void EquipWeapon(Weapon weaponToEquip)
    {
        //Check if there is already a weapon equiped
        if (equipedWeapon != null)
        {
            //Destroy(equipedWeapon.GameObject);
        }
        equipedWeapon = Instantiate(weaponToEquip, weaponHoldPosition.position, weaponHoldPosition.rotation) as Weapon;
        equipedWeapon.transform.parent = weaponHoldPosition;
    }

    public void Shoot()
    {
        if (equipedWeapon != null)
        {
            equipedWeapon.Shoot();
        }
    }
}
