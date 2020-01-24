using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Bomb : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;

    public GameObject explosion;
    public float bombRadius = 5f;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //move the projectile in a forward direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        DamageNearEnemies();
        Instantiate(explosion, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //other.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (other.tag == "Boss")
        {
            //other.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }

    }

    void DamageNearEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Enemy")
            {
                nearbyObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (nearbyObject.tag == "Boss")
            {
                nearbyObject.GetComponent<Boss>().TakeDamage(damage);
            }
        }
    }
}
