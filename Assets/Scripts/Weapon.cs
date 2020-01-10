using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get the direction of the mouse position form the weapon position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //tranform the direction to an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //convert the angle to a unity rotation
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //make the weapon rotation face the direction of the mouse cursor position
        transform.rotation = rotation;
    }
}
