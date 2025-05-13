using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public ItemData_Equipment mainHandWeapon;
    public ItemData_Equipment offHandWeapon;

    public void SwitchWeapons()
    {
        if (mainHandWeapon != null && offHandWeapon != null)
        {
            EquipmentType temp = mainHandWeapon.equipmentType;
            mainHandWeapon.EquipItemToSlot(offHandWeapon, EquipmentType.WeaponMainHand);
        }
    }
}
