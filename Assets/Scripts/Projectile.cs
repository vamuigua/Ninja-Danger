using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        //destroy the projectile after some seconds
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //move the projectile in a forward direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
