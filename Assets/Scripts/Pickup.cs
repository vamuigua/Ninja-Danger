using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public GameObject pickUpSound;

    private bool foundExistingGun = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            for (int i = 0; i < player.availableWeapons.Count; i++)
            {
                if (player.availableWeapons[i] == weaponToEquip)
                {
                    foundExistingGun = true;
                }
            }

            if (foundExistingGun == false)
            {
                Instantiate(pickUpSound, transform.position, Quaternion.identity);
                player.ChangeWeapon(weaponToEquip);
                player.addWeapon(weaponToEquip, gameObject.GetComponent<SpriteRenderer>().sprite);
                Destroy(gameObject);
            }
        }
    }
}
