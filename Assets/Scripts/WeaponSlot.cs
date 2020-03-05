using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private GameObject weaponHolder;
    private GameObject weaponPickUp;
    private GameObject player;
    private GameObject activeWeapon;

    private int activeWeaponPos;
    private int active_slot_no;
    private int activeWeaponAmmo;

    public float dropWeaponOffset = 10;
    public bool empty = true;

    void Start()
    {
        empty = true;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // On Pressing Spacebar or Clicking Right-mouse Button
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        // {
        //     // check if there exists a Weapon in the scene
        //     if (FindObjectOfType<Weapon>() != null)
        //     {
        //         DropWeapon();
        //     }
        // }
    }

    //Destroy the weaponButton in the weapon slot UI
    public void DestroyWeapon()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "WeaponButton")
            {
                GameObject.Destroy(child.gameObject);
            }
            child.gameObject.SetActive(false);
        }
    }

    public void DropWeapon()
    {
        int i = 0;
        //find active weapon in weaponholder & its weapon_UI slot position
        foreach (Transform weapon in weaponHolder.transform)
        {
            //get active weapon
            if (weapon.gameObject.activeSelf == true && weapon.tag == "Weapon")
            {
                activeWeaponPos = i;
                activeWeapon = weapon.gameObject;
                activeWeaponAmmo = activeWeapon.GetComponent<Weapon>().currentAmmo;
                active_slot_no = activeWeapon.GetComponent<Weapon>().weaponSlotPosUI;
                weaponPickUp = activeWeapon.GetComponent<Weapon>().weaponPickUp;
                Debug.Log("Got active Weapon!");
                Debug.Log("Element i ==> " + i);
                break;
            }
            else
            {
                i++;
            }
        }

        //create instance of the active weapon
        Vector2 dropWeaponPos = new Vector2(player.transform.position.x, player.transform.position.y + dropWeaponOffset);
        GameObject droppedPickup = Instantiate(weaponPickUp, dropWeaponPos, Quaternion.identity);
        Debug.Log("Made new weapon!");

        //set the current ammo of the droppedWeapon to the previous active weapon
        Weapon droppedWeapon = droppedPickup.GetComponent<Pickup>().weaponToEquip;
        droppedWeapon.maxAmmo = activeWeaponAmmo;

        // get the Active WeaponSlot Script
        WeaponSlot WeaponSlot = player.GetComponent<Player>().weaponsSlots[active_slot_no].GetComponent<WeaponSlot>();

        //set the weaponslot empty to true
        WeaponSlot.empty = true;

        //reduce the number of currentlyAvailableWeapons
        weaponHolder.GetComponent<WeaponSwitching>().currentAvailableWeapons--;

        //destroy the weapon in the weaponholder & WeaponUI
        WeaponSlot.DestroyWeapon();
        Destroy(activeWeapon);
    }
}
