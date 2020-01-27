﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;

    public Enemy[] enemies;
    public GameObject deathEffect;
    public GameObject deathSound;
    public GameObject bloodSpatter;

    private int halfHealth;

    public int health;
    public float spawnOffset;
    public int damage;

    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Instantiate(deathSound, transform.position, Quaternion.identity);
            Instantiate(bloodSpatter, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
