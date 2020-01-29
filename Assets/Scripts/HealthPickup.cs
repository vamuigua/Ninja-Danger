using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Player playerScript;
    private GameObject player;
    public GameObject healthPickupEffect;
    public GameObject healthSound;
    public int healAmount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (playerScript.health != 5)
            {
                Instantiate(healthSound, transform.position, Quaternion.identity);
                playerScript.Heal(healAmount);
                Instantiate(healthPickupEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
