using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    public GameObject[] pickups;
    public GameObject healthPickup;
    public GameObject deathEffect;

    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;
    public int pickUpChance;
    public int healthPickupChance;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {

            int choice = Random.Range(0, 2);
            if (choice == 0)
            {
                int randomNumber = Random.Range(0, 101);
                if (randomNumber < pickUpChance)
                {
                    GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);
                }
            }
            else if (choice == 1)
            {
                int randomHealthNumber = Random.Range(0, 101);
                if (randomHealthNumber < healthPickupChance)
                {
                    Instantiate(healthPickup, transform.position, transform.rotation);
                }
            }

            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
