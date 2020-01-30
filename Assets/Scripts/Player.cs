using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource source;
    private Vector2 moveAmount;
    private SceneTransitions sceneTransitionAnim;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator hurtPanel;
    public Animator cameraAnim;
    public AudioClip[] hurtSounds;

    private Weapon NextWeaponToEquip;
    private int selectedWeaponPos = 0;
    public float speed;
    public int health;

    [HideInInspector]
    public List<Weapon> availableWeapons = new List<Weapon>();
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitionAnim = FindObjectOfType<SceneTransitions>();
    }

    // Update is called once per frame
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            switchWeapon();
        }
    }

    private void FixedUpdate()
    {
        //move the player
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

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

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

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

    public void addWeapon(Weapon newWeapon)
    {
        availableWeapons.Add(newWeapon);
        // currentWeapon = availableWeapons[selectedWeaponPos];
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            print(availableWeapons[i]);
        }
    }

    public void switchWeapon()
    {
        if (availableWeapons.Count > 1)
        {
            //find next gun in the list to equip
            if (selectedWeaponPos == availableWeapons.Count - 1)
            {
                selectedWeaponPos = 0;
            }
            else
            {
                selectedWeaponPos = selectedWeaponPos + 1;
            }

            //set next gun to equip
            NextWeaponToEquip = availableWeapons[selectedWeaponPos];
            //find the current gun & destroy the current held gun
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            //equip gun to player
            Instantiate(NextWeaponToEquip, transform.position, transform.rotation, transform);
        }
    }
}
