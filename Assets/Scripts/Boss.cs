using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator anim;
    private Slider healthBar;
    private SceneTransitions sceneTransitionAnim;

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
        healthBar = GameObject.FindObjectOfType<Slider>();
        sceneTransitionAnim = FindObjectOfType<SceneTransitions>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathSound, transform.position, Quaternion.identity);
            Instantiate(bloodSpatter, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            healthBar.gameObject.SetActive(false);
            Destroy(gameObject);
            sceneTransitionAnim.OnLoadScene("Win");

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
