using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

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

        //Fire projectile
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
