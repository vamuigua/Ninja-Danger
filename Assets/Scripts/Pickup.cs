using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public GameObject pickUpSound;
    public GameObject itemButton;

    // on collision with other colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            CheckAvailableWeaponSlot(player);
        }
    }

    // checks if there is an empty slot in the weapons slot in order to add a new weapon
    private void CheckAvailableWeaponSlot(Player player)
    {
        for (int i = 0; i < player.weaponsSlots.Length; i++)
        {
            bool isEmpty = player.weaponsSlots[i].GetComponent<WeaponSlot>().empty;
            WeaponSwitching weaponSwitching = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitching>();

            // checks for the status of the weaponSlot at index 'i' and if the max no. of weapons to carry has been reached
            if (isEmpty && weaponSwitching.currentAvailableWeapons < weaponSwitching.maxWeaponsToCarry)
            {
                Instantiate(pickUpSound, transform.position, Quaternion.identity);
                GameObject weaponButton = Instantiate(itemButton, player.weaponsSlots[i].transform, false);
                player.weaponsSlots[i].GetComponent<WeaponSlot>().empty = false;
                player.addWeapon(weaponToEquip, weaponButton, i);
                Destroy(gameObject);
                break;
            }
        }
    }
}
