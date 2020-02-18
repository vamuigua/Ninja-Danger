using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AudioSource source;

    public GameObject projectile;
    public GameObject reloadSound;
    public Transform shotPoint;


    public float timeBetweenShots;

    private float shotTime;
    private int currentAmmo;
    private bool isReloading = false;

    public float reloadTime = 2f;
    public int maxAmmo = 10;

    void Start()
    {
        currentAmmo = maxAmmo;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the direction of the mouse position form the weapon position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //tranform the direction to an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //convert the angle to a unity rotation
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //make the weapon rotation face the direction of the mouse cursor position
        transform.rotation = rotation;

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //Fire projectile
        if (Input.GetMouseButton(0))
        {
            shoot();
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        Instantiate(reloadSound, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void shoot()
    {
        if (Time.time >= shotTime)
        {
            currentAmmo--;
            source.Play();
            Instantiate(projectile, shotPoint.position, transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
