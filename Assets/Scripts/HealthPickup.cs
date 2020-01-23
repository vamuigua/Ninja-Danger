using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Player playerScript;
    public GameObject healthPickupEffect;
    public int healAmount;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerScript.Heal(healAmount);
            Instantiate(healthPickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
