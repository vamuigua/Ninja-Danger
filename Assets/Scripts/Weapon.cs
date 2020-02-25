using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    private AudioSource source;
    private Player player;
    private WeaponSwitching weaponSwitching;

    public GameObject projectile;
    public GameObject reloadSound;
    public Transform shotPoint;

    private float shotTime;
    private int currentAmmo;

    public float timeBetweenShots;
    public float reloadTime = 2f;
    public int maxAmmo = 10;
    public int currentWeaponSlot;
    public int weaponSlotPosUI;
    public TextMeshProUGUI ammoTextMesh;

    void Start()
    {
        currentAmmo = maxAmmo;
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weaponSwitching = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitching>();
        ammoTextMesh.SetText(maxAmmo.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //get the direction of the mouse position form the weapon position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //tranform the direction to an angles
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //convert the angle to a unity rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //make the weapon rotation face the direction of the mouse cursor position
        transform.rotation = rotation;

        //check if gun ammo is replenished
        if (currentAmmo <= 0 && player != null)
        {
            weaponSwitching.currentAvailableWeapons--;

            //get the WeaponSlot for the current active weapon
            WeaponSlot weaponSlot = player.weaponsSlots[currentWeaponSlot].GetComponent<WeaponSlot>();
            weaponSlot.empty = true;
            weaponSlot.DropItem();

            //check if only one weapon is left in the weapon slot & make it active
            if (weaponSwitching.currentAvailableWeapons == 1)
            {
                foreach (Transform weapon in weaponSwitching.transform)
                {
                    if (weapon.gameObject.activeSelf == false)
                    {
                        weapon.gameObject.SetActive(true);
                        int slot = weapon.gameObject.GetComponent<Weapon>().weaponSlotPosUI;
                        GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitching>().activeSlotImage[slot].SetActive(true);
                    }
                }
            }

            Destroy(gameObject);
        }

        //Fire projectile
        if (Input.GetMouseButton(0))
        {
            shoot();
        }
    }

    //fire the weapon projectile
    private void shoot()
    {
        if (Time.time >= shotTime)
        {
            currentAmmo--;
            ammoTextMesh.SetText(currentAmmo.ToString());
            source.Play();
            Instantiate(projectile, shotPoint.position, transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
