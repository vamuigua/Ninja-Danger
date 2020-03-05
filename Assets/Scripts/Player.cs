using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource source;
    private Vector2 moveAmount;
    private SceneTransitions sceneTransitionAnim;

    public GameObject WeaponHolder;
    public Image[] hearts;
    public GameObject[] weaponsSlots;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator hurtPanel;
    public Animator cameraAnim;
    public AudioClip[] hurtSounds;

    public float speed;
    public int health;
    public int waitWinSec;
    public bool bossIsDead;

    [HideInInspector]
    public List<Weapon> availableWeapons = new List<Weapon>();

    void Start()
    {
        bossIsDead = false;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitionAnim = FindObjectOfType<SceneTransitions>();
    }

    void Update()
    {
        //check the move input from the player
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        //play the required player animation
        if (moveInput != Vector2.zero)
        {
            cameraAnim.SetBool("isMoving", true);
            anim.SetBool("isRunning", true);
        }
        else
        {
            cameraAnim.SetBool("isMoving", false);
            anim.SetBool("isRunning", false);
        }

        //destroy all enemies when the boss is dead
        if (bossIsDead == true && FindObjectOfType<Enemy>() != null)
        {
            FindObjectOfType<Enemy>().destroyEnemy();
            StartCoroutine(waitDeathSeconds(waitWinSec));
        }
    }

    //FixedUpdate used when Physics is involved e.g when an object has a rigidbody
    private void FixedUpdate()
    {
        //move the player
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    // makes the player to take damage
    public void TakeDamage(int damageAmount)
    {
        int randNum = Random.Range(0, hurtSounds.Length);
        source.clip = hurtSounds[randNum];
        source.Play();

        hurtPanel.SetTrigger("hurt");
        health -= damageAmount;
        UpdateHealthUI(health);

        if (health == 1)
        {
            hurtPanel.SetTrigger("critical");
        }
        else if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitionAnim.OnLoadScene("Lose");
        }
    }

    // updates the Health UI Status
    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    //Function that heals the Player when a life id obtained 
    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
            if (health > 1)
            {
                hurtPanel.SetTrigger("idle");
            }
        }
        UpdateHealthUI(health);
    }

    // adds the obtained Weapon to the weapons slot
    public void addWeapon(Weapon newWeapon, GameObject weaponButton, int Slot_i)
    {
        Weapon newGun = Instantiate(newWeapon, transform.position, Quaternion.identity, WeaponHolder.transform);
        newGun.currentWeaponSlot = Slot_i;
        newGun.ammoTextMesh = weaponButton.GetComponentInChildren<TextMeshProUGUI>();
        newGun.weaponSlotPosUI = Slot_i;
        WeaponSwitching weaponSwitching = WeaponHolder.GetComponent<WeaponSwitching>();
        weaponSwitching.currentAvailableWeapons++;
        weaponSwitching.SelectWeapon();
    }

    //wait for some seconds before playing the win scene
    IEnumerator waitDeathSeconds(int waitWinSec)
    {
        yield return new WaitForSeconds(waitWinSec);
        sceneTransitionAnim.OnLoadScene("Win");
    }
}
