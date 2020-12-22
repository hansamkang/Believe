using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D bulletRigidbody;
    public float bulletSpeed = 20f;

    void Start()
    {

        bulletRigidbody.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collider.gameObject.tag == "EnemyBoss")
        {
            Destroy(gameObject);
        }
    }
}
