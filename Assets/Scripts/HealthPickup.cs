using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Player playerScript;
    public GameObject healthPickupEffect;
    public GameObject healthSound;
    public int healAmount;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(healthSound, transform.position, Quaternion.identity);
            playerScript.Heal(healAmount);
            Instantiate(healthPickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
