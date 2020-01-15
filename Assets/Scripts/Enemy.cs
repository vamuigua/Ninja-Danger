using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
